using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AgricultureManager.Core.Domain.Entities;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.FieldFeatures
{
    public class AddFieldCommand : IReq<FieldVm>
    {
        public string Number { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public float Area { get; set; }
        public string? Comment { get; set; }
    }
    public class AddFieldCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<AddFieldCommand, FieldVm>
    {
        public async Task<Response<FieldVm>> Handle(AddFieldCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var field = mapper.Map<Field>(request);
            field.Id = Guid.NewGuid();

            await dbContext.Field.AddAsync(field, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            var fieldVm = mapper.Map<FieldVm>(field);
            return Response.Success(fieldVm);
        }
    }
}
