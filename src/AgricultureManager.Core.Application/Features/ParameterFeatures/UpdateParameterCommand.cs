using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.ParameterFeatures
{
    public class UpdateParameterCommand : IReq<ParameterVm>
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
    //public class UpdateParameterCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<UpdateParameterCommand, ParameterVm>
    //{
    //    public async Task<ParameterVm> Handle(UpdateParameterCommand request, CancellationToken cancellationToken)
    //    {
    //        var existEntity = await dbContext.Parameter.FindAsync([request.Key], cancellationToken)
    //            ?? throw new MediatorHandleException($"Parameter mit Key {request.Key} wurde nicht gefunden.");

    //        mapper.Map(request, existEntity);

    //        var entry = dbContext.Parameter.Update(existEntity);
    //        await dbContext.SaveChangesAsync(cancellationToken);

    //        return mapper.Map<ParameterVm>(entry.Entity);
    //    }
    //}
    public class UpdateParameterCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateParameterCommand, ParameterVm>
    {
        public async Task<Response<ParameterVm>> Handle(UpdateParameterCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();
            var existEntity = await dbContext.Parameter.FindAsync([request.Key], cancellationToken);
            if (existEntity is null)
                return Response.Fail<ParameterVm>($"Parameter mit Key {request.Key} wurde nicht gefunden.");

            mapper.Map(request, existEntity);

            var entry = dbContext.Parameter.Update(existEntity);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Response.Success(mapper.Map<ParameterVm>(entry.Entity));
        }
    }

}
