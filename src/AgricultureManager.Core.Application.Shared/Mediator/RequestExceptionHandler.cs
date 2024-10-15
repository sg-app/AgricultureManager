using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace AgricultureManager.Core.Application.Shared.Mediator
{
    public class RequestExceptionHandler<TRequest, TResponse, TException>(ILoggerFactory loggerFactory) : IRequestExceptionHandler<TRequest, TResponse, TException>
        where TResponse : class
        where TRequest : IRequest<TResponse>
        where TException : Exception
    {

        public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
        {
            var logger = loggerFactory.CreateLogger(typeof(TRequest).Namespace ?? "UnknownNamespace");

            logger.LogError(exception, "Error occured!");

            var responseType = typeof(TResponse);
            if (responseType.IsGenericType)
            {
                //var resultType = responseType.GetGenericArguments()[0];
                //var invalidResponseType = typeof(ResponseContext<>).MakeGenericType(resultType);

                //var invalidResponse = Activator.CreateInstance(invalidResponseType, [false, exception.Message, null]);

                //if (invalidResponse is TResponse response)
                //    state.SetHandled(response);
            }

            return Task.CompletedTask;
        }

    }
}
