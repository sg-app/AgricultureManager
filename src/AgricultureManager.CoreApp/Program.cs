using AgricultureManager.Core.Application;
using AgricultureManager.Core.Application.Services;
using AgricultureManager.Core.Application.Shared.States;
using AgricultureManager.CoreApp.Components;
using AgricultureManager.Infrastructure.Persistence;
using AgricultureManager.Module.Manager;
using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using LiveChartsCore;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using Radzen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddNLog();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(f => f.FullName is not null && f.FullName.Contains("AgricultureManager.Module", StringComparison.OrdinalIgnoreCase)).ToList();
assemblies.Add(Assembly.GetAssembly(typeof(HarvestYearState))!);


builder.Services.AddRadzenComponents();
builder.Services.AddPlugins(builder.Configuration);
builder.Services.RegisterMasterdata();
builder.Services.AddCoreApplication(builder.Configuration);
builder.Services.AddCorePersistence(builder.Configuration);

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication("Negotiate")
    .AddNegotiate();

builder.Services.AddAuthorizationCore();

builder.Services.AddFluxor(config =>
{
    config.ScanAssemblies(
        Assembly.GetExecutingAssembly(),
        [
            ..assemblies
        ]);
#if DEBUG
    config.UseReduxDevTools();
#endif
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    // Apply migrations at startup
    using var scope = app.Services.CreateScope();
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
