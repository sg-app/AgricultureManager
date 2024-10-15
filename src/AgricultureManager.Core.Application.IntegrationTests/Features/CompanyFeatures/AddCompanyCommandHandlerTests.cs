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
            result.CompanyName.Should().Be(request.CompanyName);
            result.FirstName.Should().Be(request.FirstName);
            result.LastName.Should().Be(request.LastName);
            result.Street.Should().Be(request.Street);
            result.Housenumber.Should().Be(request.Housenumber);
            result.Plz.Should().Be(request.Plz);
            result.City.Should().Be(request.City);
            result.CompanyNumber.Should().Be(request.CompanyNumber);
        }
    }
}
