using System.Globalization;
using System.Text;
using BusManagement.Core.Common.Helpers;
using BusManagement.Core.Data;
using BusManagement.Infrastructure.Context;
using BusManagement.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;

namespace BusManagement.Presentation.API;

public static class StartupExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddInfrastructureServices();
        var connectionString =
            builder.Configuration.GetConnectionString("OffDutyDbContextConnection")
            ?? throw new InvalidOperationException(
                "Connection string 'OffDutyDbContextConnection' not found."
            );
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString, x => x.UseNetTopologySuite())
        );
        builder
            .Services.AddIdentity<ApplicationUser, IdentityRole>()
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

        builder.Services.AddControllers();
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
        builder.Services.AddSwaggerGen();
        return builder.Build();
    }
}
