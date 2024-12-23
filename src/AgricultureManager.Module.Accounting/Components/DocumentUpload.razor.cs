using AgricultureManager.Module.Accounting.Features.DocumentFeatures;
using AgricultureManager.Module.Accounting.Models;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace AgricultureManager.Module.Accounting.Components
{
    public partial class DocumentUpload
    {
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;
        [Parameter] public AccountMouvementVm AccountMouvement { get; set; } = default!;

        private string _customDescription = string.Empty;
        private ICollection<DocumentVm> _documents = [];
        private bool _isLoading = false;

        private IJSObjectReference? _filePasteModule;
        private IJSObjectReference? _filePasteFunctionReference;

        private ElementReference _fileDropContainer;
        private InputFile inputFile = default!;
        private string? HoverClass;
        private Guid _inputFieldKey = Guid.NewGuid();
        private IBrowserFile? _file;

        protected override async Task OnParametersSetAsync()
        {
            _isLoading = true;
            var response = await Mediator.Send(new GetDocumentListByMouvementIdCommand(AccountMouvement.Id));
            _documents = response.Data ?? [];
            _isLoading = false;
        }
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _filePasteModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/filePaste.js");
                _filePasteFunctionReference = await _filePasteModule.InvokeAsync<IJSObjectReference>("initializeFilePaste", _fileDropContainer, inputFile.Element);
            }
        }
        void OnDragEnter(DragEventArgs e) => HoverClass = "hover";

        void OnDragLeave(DragEventArgs e) => HoverClass = string.Empty;


        private void OnChange(InputFileChangeEventArgs args)
        {
            _file = args.File;
            HoverClass = string.Empty;
        }

        private async Task StartUpload()
        {
            _isLoading = true;

            if (AccountMouvement != null && _file != null)
            {

                var response = await Mediator.Send(new UploadDocumentCommand(AccountMouvement, _file, _customDescription));
                if (response.Success && response.Data != null)
                {
                    _documents.Add(response.Data);
                    _file = null;
                    _customDescription = String.Empty;
                    _inputFieldKey = Guid.NewGuid();
                }
            }
            _isLoading = false;
        }
        private async Task Remove(DocumentVm item)
        {
            var response = await Mediator.Send(new RemoveDocumentCommand(item.Id));
            if (response.Success)
                _documents.Remove(item);
        }

        private async Task Download(DocumentVm item)
        {
            _isLoading = true;
            var response = await Mediator.Send(new DownloadDocumentCommand(item.Id));
            if (response.Success && response.Data != null)
            {
                // All OK. Download file!
                await JSRuntime.InvokeVoidAsync("downloadFileFromByteArray", item.Documentname, response.Data);
            }
            _isLoading = false;
        }

    }
}
