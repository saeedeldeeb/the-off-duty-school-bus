using BusManagement.Presentation.API;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureServices();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var supportedCultures = new[] { "ar-EG", "en-US" };
app.UseRequestLocalization(options =>
    options.SetDefaultCulture(supportedCultures[0]).AddSupportedCultures(supportedCultures)
);

app.UseCors("AllowAll");
app.UseStaticFiles();
app.MapGet("/hello", () => "Hello World!");
app.MapControllerRoute(name: "default", pattern: "{controller=Auth}/{action=Login}/{id?}");
app.UseAuthentication();
app.UseAuthorization();

app.Run();
