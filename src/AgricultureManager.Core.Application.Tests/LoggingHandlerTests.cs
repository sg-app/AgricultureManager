using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Mediator;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace AgricultureManager.Core.Application.Tests
{
    [TestFixture]
    public class LoggingHandlerTests
    {
        private Mock<ILoggerFactory> _loggerFactoryMock;
        private Mock<ILogger> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _loggerFactoryMock = new Mock<ILoggerFactory>();
            _loggerMock = new Mock<ILogger>();
            _loggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(_loggerMock.Object);
        }

        [Test]
        public async Task Handle_LogsRequestAndResponse()
        {
            // Arrange
            var request = new Mock<ITraceable>();
            var response = new object();
            var handler = new LoggingHandler<ITraceable, object>(_loggerFactoryMock.Object);

            // Act
            await handler.Handle(request.Object, () => Task.FromResult(response), CancellationToken.None);

            // Assert
            _loggerMock.VerifyLog(x => x.LogTrace(It.Is<string>(s => s.Contains("Handling request")), It.IsAny<object[]>()), Times.Once);
            _loggerMock.VerifyLog(x => x.LogTrace(It.Is<string>(s => s.Contains("Response for")), It.IsAny<object[]>()), Times.Once);
        }

        [Test]
        public async Task Handle_LogsRequestWithCorrectId()
        {
            // Arrange
            var request = new Mock<ITraceable>();
            var response = new object();
            var handler = new LoggingHandler<ITraceable, object>(_loggerFactoryMock.Object);

            // Act
            await handler.Handle(request.Object, () => Task.FromResult(response), CancellationToken.None);

            // Assert
            _loggerMock.VerifyLog(x => x.LogTrace(It.Is<string>(s => s.Contains("Handling request")), It.Is<object[]>(o => o[0] is Guid)), Times.Once);
        }

        [Test]
        public async Task Handle_LogsResponseWithCorrectId()
        {
            // Arrange
            var request = new Mock<ITraceable>();
            var response = new object();
            var handler = new LoggingHandler<ITraceable, object>(_loggerFactoryMock.Object);

            // Act
            await handler.Handle(request.Object, () => Task.FromResult(response), CancellationToken.None);

            // Assert
            _loggerMock.VerifyLog(x => x.LogTrace(It.Is<string>(s => s.Contains("Response for")), It.Is<object[]>(o => o[0] is Guid)), Times.Once);
        }

        [Test]
        public async Task Handle_LogsRequestWithCorrectName()
        {
            // Arrange
            var request = new Mock<ITraceable>();
            var response = new object();
            var handler = new LoggingHandler<ITraceable, object>(_loggerFactoryMock.Object);

            // Act
            await handler.Handle(request.Object, () => Task.FromResult(response), CancellationToken.None);

            // Assert
            _loggerMock.VerifyLog(x => x.LogTrace(It.Is<string>(s => s.Contains("Handling request")), It.Is<object[]>(o => o[1].ToString() == nameof(ITraceable))), Times.Once);
        }

        [Test]
        public async Task Handle_LogsResponseWithCorrectName()
        {
            // Arrange
            var request = new Mock<ITraceable>();
            var response = new object();
            var handler = new LoggingHandler<ITraceable, object>(_loggerFactoryMock.Object);

            // Act
            await handler.Handle(request.Object, () => Task.FromResult(response), CancellationToken.None);

            // Assert
            _loggerMock.VerifyLog(x => x.LogTrace(It.Is<string>(s => s.Contains("Response for")), It.Is<object[]>(o => o[1].ToString() == nameof(ITraceable))), Times.Once);
        }

    }
}
