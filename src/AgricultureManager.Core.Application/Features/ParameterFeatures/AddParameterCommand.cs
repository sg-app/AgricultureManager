using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.ParameterFeatures
{
    public class AddParameterCommand : IReq<ParameterVm>
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
    //public class AddParameterCommandHandler(IAppDbContext dbContext, IMapper mapper) : IRequestHandler<AddParameterCommand, ParameterVm>
    //{
    //    public async Task<ParameterVm> Handle(AddParameterCommand request, CancellationToken cancellationToken)
    //    {

    //        var existEntiy = await dbContext.Parameter.FindAsync([request.Key], cancellationToken);

    //        if (existEntiy is not null)
    //            throw new MediatorHandleException($"Parameter mit Key {request.Key} existiert bereits.");

    //        var entity = mapper.Map<Parameter>(request);

    //        var entry = dbContext.Parameter.Add(entity);
    //        await dbContext.SaveChangesAsync(cancellationToken);

    //        return mapper.Map<ParameterVm>(entry.Entity);
    //    }
    //}

    public class AddParameterCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddParameterCommand, ParameterVm>
    {
        public async Task<Response<ParameterVm>> Handle(AddParameterCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var existEntiy = await dbContext.Parameter.FindAsync([request.Key], cancellationToken);

            if (existEntiy is not null)
                return Response.Fail<ParameterVm>($"Parameter mit Key {request.Key} existiert bereits.");

            var entity = mapper.Map<Parameter>(request);

            var entry = dbContext.Parameter.Add(entity);
            await dbContext.SaveChangesAsync(cancellationToken);

            return Response.Success(mapper.Map<ParameterVm>(entry.Entity));
        }
    }
}
