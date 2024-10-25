using AgricultureManager.Core.Application.Shared.Interfaces.Mediator;
using AgricultureManager.Core.Application.Shared.Interfaces.Persistence;
using AgricultureManager.Core.Application.Shared.Models;
using AutoMapper;

namespace AgricultureManager.Core.Application.Features.FieldFeatures
{
    public class UpdateFieldCommand : IReq<FieldVm>
    {
        public Guid Id { get; set; }
        public string Number { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public float Area { get; set; }
        public string? Comment { get; set; }
    }

    public class UpdateFieldCommandHandler(IAppDbContextFactory dbContextFactory, IMapper mapper) : IReqHandler<UpdateFieldCommand, FieldVm>
    {
        public async Task<Response<FieldVm>> Handle(UpdateFieldCommand request, CancellationToken cancellationToken)
        {
            using var dbContext = dbContextFactory.CreateDbContext();

            var field = await dbContext.Field.FindAsync([request.Id], cancellationToken);
            if (field is null)
            {
                return Response.Fail<FieldVm>("Schlag nicht gefunden.");
            }

            mapper.Map(request, field);

            dbContext.Field.Update(field);
            await dbContext.SaveChangesAsync(cancellationToken);

            var fieldVm = mapper.Map<FieldVm>(field);
            return Response.Success(fieldVm);
        }
    }
}
