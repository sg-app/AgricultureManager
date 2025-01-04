using AgricultureManager.Module.Accounting.Features.StatementOfAccountFeatures;
using AgricultureManager.Module.Accounting.Store.States;
using Fluxor;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace AgricultureManager.Module.Accounting.Components
{
    public partial class UploadStatementOfAccount
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IJSRuntime JSRuntime { get; set; } = default!;
        [Inject] public IState<AccountState> AccountState { get; set; } = default!;
        [Parameter] public EventCallback UploadDone { get; set; }

        private IJSObjectReference _filePasteModule;
        private IJSObjectReference _filePasteFunctionReference;

        private ElementReference _fileDropContainer;
        private InputFile inputFile;
        private string HoverClass;
        private Guid _inputFieldKey = Guid.NewGuid();

        private int _selectedYear;
        private int _selectedMonth;
        private bool _overwrite;

        private IBrowserFile? _file;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            _selectedMonth = DateTime.UtcNow.AddMonths(-1).Month;
            _selectedYear = _selectedMonth != 12 ? DateTime.UtcNow.Year : DateTime.UtcNow.AddYears(-1).Year;
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
            if (_file != null && AccountState.Value.SelectedAccount != null)
            {
                var response = await Mediator.Send(new UploadStatementOfAccountCommand(AccountState.Value.SelectedAccount.Id, _selectedMonth, _selectedYear, _file, _overwrite));
                if (response.Success && response.Data != null)
                {
                    _file = null;
                    _inputFieldKey = Guid.NewGuid();
                    await UploadDone.InvokeAsync();
                }
            }
        }

        protected override async ValueTask DisposeAsyncCore(bool disposing)
        {
            await base.DisposeAsyncCore(disposing);
            if (disposing)
            {
                if (_filePasteFunctionReference != null)
                {
                    await _filePasteFunctionReference.InvokeVoidAsync("dispose");
                    await _filePasteFunctionReference.DisposeAsync();
                }

                if (_filePasteModule != null)
                {
                    await _filePasteModule.DisposeAsync();
                }
            }
        }

        private static IEnumerable<int> GetYears()
        {
            int currentYear = DateTime.Now.Year;
            return Enumerable.Range(currentYear - 10, 11);
        }

        private static IEnumerable<int> GetMonth()
            => Enumerable.Range(1, 12);

    }
}
