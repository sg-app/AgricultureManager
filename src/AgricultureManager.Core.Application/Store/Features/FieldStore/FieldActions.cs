using AgricultureManager.Core.Application.Shared.Models;

namespace AgricultureManager.Core.Application.Store.Features.FieldStore
{
    public record LoadFieldsDataAction();
    public record LoadFieldsDataResultAction(IEnumerable<FieldVm> Fields);
    public record LoadFieldDataResultFailAction();
    public record AddFieldAction(FieldVm Field);
    public record UpdateFieldAction(FieldVm Field);
    public record RemoveFieldAction(Guid FieldId);
}
