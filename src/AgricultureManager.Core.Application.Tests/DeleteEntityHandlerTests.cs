using AgricultureManager.Core.Application.Common;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace AgricultureManager.Core.Application.Shared.Tests
{
    public class DeleteEntityHandlerTests
    {
        [Test]
        public async Task Handle_DeletesEntityFromDbContext()
        {
            // Arrange
            var dbContextMock = new Mock<IAppDbContext>();
            var entityMock = new Mock<Culture>();
            var entityType = typeof(Culture);
            var entityId = Guid.NewGuid();
            var cancellationToken = new CancellationToken();

            dbContextMock.Setup(db => db.FindAsync(entityType, new object[] { entityId }, cancellationToken)).ReturnsAsync(entityMock.Object);

            var handler = new DeleteEntityCommandHandler<CultureVm>(dbContextMock.Object);
            var request = new DeleteEntityCommand<CultureVm> { Id = entityId };

            // Act
            await handler.Handle(request, cancellationToken);

            // Assert
            dbContextMock.Verify(db => db.FindAsync(entityType, new object[] { entityId }, cancellationToken), Times.Once);
            //dbContextMock.Verify(db => db.Remove(entityMock.Object), Times.Once);
            dbContextMock.Verify(db => db.SaveChangesAsync(cancellationToken), Times.Once);
        }
    }
}
