using BusManagement.Presentation.API;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfigureServices();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/hello", () => "Hello World!");
app.MapControllerRoute(name: "default", pattern: "{controller=Auth}/{action=Login}/{id?}");
app.UseAuthentication();
app.UseAuthorization();

app.Run();
