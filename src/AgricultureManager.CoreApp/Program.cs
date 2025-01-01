using AgricultureManager.Core.Application;
using AgricultureManager.CoreApp.Components;
using AgricultureManager.Infrastructure.Persistence;
using AgricultureManager.Module.Manager;
using Microsoft.EntityFrameworkCore;
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

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UsePlugins();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddPluginAssemblies(app.Services);

app.Run();
