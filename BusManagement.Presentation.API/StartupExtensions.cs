using System.Globalization;
using System.Text;
using BusManagement.Core.Common.Helpers;
using BusManagement.Core.Data;
using BusManagement.Infrastructure.Context;
using BusManagement.Infrastructure.DataStructureMapping;
using BusManagement.Infrastructure.Repositories;
using BusManagement.Infrastructure.Services;
using BusManagement.Presentation.API.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;

namespace BusManagement.Presentation.API;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddInfrastructureServices();
        builder.Services.AddInfrastructureRepositories();
        builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        builder.Services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        var connectionString =
            builder.Configuration.GetConnectionString("OffDutyDbContextConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'OffDutyDbContextConnection' not found."
            );
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString, x => x.UseNetTopologySuite())
        );
        builder
            .Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.ClaimsIdentity.UserIdClaimType = "uid";
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
        builder
            .Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])
                    )
                };
            });

        builder
            .Services.AddControllers()
            .AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
            });
        builder.Services.AddDateOnlyTimeOnlyStringConverters();
        builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
        builder.Services.AddDistributedMemoryCache();
        builder
            .Services.AddMvc()
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (_, factory) =>
                    factory.Create(typeof(JsonStringLocalizerFactory));
            });
        // Add services to the container.
        StructureMapper.Initialize();
        builder.Services.AddAutoMapper(typeof(Program).Assembly);
        builder.Services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();

        builder.Services.AddLocalization();
        builder.Services.AddDistributedMemoryCache();
        builder
            .Services.AddMvc()
            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (_, factory) =>
                    factory.Create(typeof(JsonStringLocalizerFactory));
            });
        var supportedCultures = new[] { "ar-EG", "en-US" };
        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(culture: new CultureInfo("ar-EG"));
            options.SupportedCultures = supportedCultures.Select(c => new CultureInfo(c)).ToList();
        });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Bus Management API", Version = "v1" });
            opt.AddSecurityDefinition(
                "Bearer",
                new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                }
            );
            opt.AddSecurityRequirement(
                new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                }
            );
            opt.UseDateOnlyTimeOnlyStringConverters();
        });
        return builder.Build();
    }
}
