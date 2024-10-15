using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace AgricultureManager.Core.Application.Shared.Mediator
{
    /// <summary>  
    /// Pipeline behavior for logging requests and responses.  
    /// </summary>  
    /// <typeparam name="TRequest">The type of the request.</typeparam>  
    /// <typeparam name="TResponse">The type of the response.</typeparam>  
    public class LoggingHandler<TRequest, TResponse>(ILoggerFactory loggerFactory) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, ITraceable
    {
        /// <summary>  
        /// Handles the logging of the request and response.  
        /// </summary>  
        /// <param name="request">The request object.</param>  
        /// <param name="next">The delegate to the next handler in the pipeline.</param>  
        /// <param name="cancellationToken">The cancellation token.</param>  
        /// <returns>The response object.</returns>  
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var logger = loggerFactory.CreateLogger(typeof(TRequest).Namespace ?? "UnknownNamespace");

            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
            };

            var requestName = request.GetType().Name;
            var requestId = Guid.NewGuid();

            // Log the request  
            var requestJson = JsonSerializer.Serialize(request, options);
            logger.LogTrace("Handling request [{id}] - {requestName}: {request}", [requestId, requestName, requestJson]);

            var response = await next();

            // Log the response  
            var responseJson = JsonSerializer.Serialize(response, options);
            logger.LogTrace("Response for [{id}] - {requestName}: {response}", [requestId, requestName, responseJson]);
            return response;
        }
    }
}
