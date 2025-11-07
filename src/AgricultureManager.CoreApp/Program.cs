using AgricultureManager.Core.Application;
using AgricultureManager.Core.Application.Services;
using AgricultureManager.Core.Application.Shared.Interfaces.Services;
using AgricultureManager.CoreApp.Components;
using AgricultureManager.CoreApp.Configuration;
using AgricultureManager.Infrastructure.Persistence;
using AgricultureManager.Module.Manager;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddNLog();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRadzenComponents();
builder.Services.AddPlugins(builder.Configuration);
builder.Services.RegisterMasterdata();
builder.Services.AddCoreApplication(builder.Configuration);
builder.Services.AddCorePersistence(builder.Configuration);

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication("Negotiate")
    .AddNegotiate();

builder.Services.AddAuthorizationCore();

builder.Services.AddFluxorRegistration();

var app = builder.Build();


// Configure the HTTP request pipeline.
using var scope = app.Services.CreateScope();
var masterdataService = scope.ServiceProvider.GetRequiredService<IMasterdataService>();
await masterdataService.InitializeAsync();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    // Apply migrations at startup
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
    app.MigratePluginDatabase();
}
app.InitializeApplication();
//app.UseHttpsRedirection();

app.MapStaticAssets();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();
app.UsePlugins();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddPluginAssemblies(app.Services);

app.Run();
