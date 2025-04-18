using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AgricultureManager.Core.Application.Services
{
    public static class InitializeApplicationExtensions
    {
        public static WebApplication InitializeApplication(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var dbContext = scope.ServiceProvider.GetRequiredService<IAppDbContextFactory>();
            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("InitializeApplication");

            logger.LogInformation("InitializeApplication started");
            
            using var context = dbContext.CreateDbContext();
            var userExists = context.User.Any();
            logger.LogInformation("User exists: {userExists}", userExists);

            var username = configuration.GetValue("Admin:Username", string.Empty);
            var password = configuration.GetValue("Admin:Password", string.Empty);
            if (!userExists && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                logger.LogInformation("Creating user {username}", username);
            
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                var addUserCommand = new Features.IdentityFeatures.AddUserCommand(username, null, null, password);
                mediator.Send(addUserCommand).GetAwaiter().GetResult();
            }
            return app;
        }
    }

}
