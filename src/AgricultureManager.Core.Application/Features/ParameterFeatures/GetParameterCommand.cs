using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Features.ParameterFeatures
{
    public class GetParameterCommand : IReq<string>
    {
        public string Key { get; set; } = string.Empty;
    }
    //public class GetParameterCommandHandler(IAppDbContext dbContext) : IRequestHandler<GetParameterCommand, string>
    //{
    //    public async Task<string> Handle(GetParameterCommand request, CancellationToken cancellationToken)
    //    {
    //        var keyValue = await dbContext.Parameter.FindAsync([request.Key], cancellationToken)
    //            ?? throw new MediatorHandleException($"Parameter mit Key {request.Key} wurde nicht gefunden.");
    //        return keyValue.Value;
    //    }
    //}

    public class GetParameterCommandHandler(IAppDbContextFactory dbContextFactory) : IReqHandler<GetParameterCommand, string>
    {
        public async Task<Response<string>> Handle(GetParameterCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var keyValue = await dbContext.Parameter.FindAsync([request.Key], cancellationToken);
            if (keyValue is null)
                return Response.Fail<string>($"Parameter mit Key {request.Key} wurde nicht gefunden.");

            return Response.Success(keyValue.Value);
        }
    }
}
