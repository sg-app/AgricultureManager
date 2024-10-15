using AgricultureManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace AgricultureManager.Core.Application.IntegrationTests
{
    [SetUpFixture]
    public class TestSetup
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            var serviceCollection = new ServiceCollection();

            // Load configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("testing.appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<TestSetup>()
                .Build();

            // Register application services
            serviceCollection.AddCoreApplication(configuration);
            serviceCollection.AddCorePersistence(configuration);

            // Build the service provider
            ServiceProvider = serviceCollection.BuildServiceProvider();

            SetupDatabase();
        }

        private static void SetupDatabase()
        {
            var dbContextFactory = ServiceProvider.GetRequiredService<IAppDbContextFactory>();
            using var dbContext = dbContextFactory.CreateDbContext();
            ((AppDbContext)dbContext).Database.EnsureCreated();
        }

        private static void DropDatabase()
        {
            var dbContextFactory = ServiceProvider.GetRequiredService<IAppDbContextFactory>();
            using var dbContext = dbContextFactory.CreateDbContext();
            ((AppDbContext)dbContext).Database.EnsureDeleted();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = ServiceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();
            return await mediator.Send(request);
        }

        [OneTimeTearDown]
        public void GlobalTeardown()
        {
            DropDatabase();
            ServiceProvider?.Dispose();
        }

    }
}
