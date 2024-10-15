using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AgricultureManager.Core.Application.Shared.Mediator
{
    public class ValidationHandler<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators, ILoggerFactory loggerFactory) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var result = await _validators.First().ValidateAsync(request, cancellationToken);

            if (!result.IsValid)
            {
                var errors = result.ToDictionary();
                var logger = loggerFactory.CreateLogger("ValidationBehaviour");
                logger.LogWarning("Validation Error for request {request}, Errors: {errors}", [nameof(request), errors]);

                var responseType = typeof(TResponse);

                if (responseType.IsGenericType)
                {
                    //var resultType = responseType.GetGenericArguments()[0];
                    //var invalidResponseType = typeof(ResponseContext<>).MakeGenericType(resultType);

                    //var invalidResponse = Activator.CreateInstance(invalidResponseType, [false, "ValidationError", errors]) as TResponse;
                    //return invalidResponse!;
                }
            }

            return await next();
        }
    }
}
