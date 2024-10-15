using AgricultureManager.Core.Application.Features.CompanyFeatures;

namespace AgricultureManager.Core.Application.IntegrationTests.Features.CompanyFeatures
{
    public class AddCompanyCommandHandlerTests
    {

        [Test]
        public async Task Handle_ShouldAddCompany_WhenRequestIsValid()
        {
            // Arrange
            var request = new AddCompanyCommand
            {
                CompanyName = "Test Company",
                FirstName = "John",
                LastName = "Doe",
                Street = "123 Main St",
                Housenumber = "1A",
                Plz = "12345",
                City = "Test City",
                CompanyNumber = "1234567890"
            };
            // Act
            var result = await TestSetup.SendAsync(request);

            // Assert
            result.Should().NotBeNull();
            result.Data.CompanyName.Should().Be(request.CompanyName);
            result.Data.FirstName.Should().Be(request.FirstName);
            result.Data.LastName.Should().Be(request.LastName);
            result.Data.Street.Should().Be(request.Street);
            result.Data.Housenumber.Should().Be(request.Housenumber);
            result.Data.Plz.Should().Be(request.Plz);
            result.Data.City.Should().Be(request.City);
            result.Data.CompanyNumber.Should().Be(request.CompanyNumber);
        }
    }
}
