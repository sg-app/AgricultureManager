using AgricultureManager.Module.Accounting.Features.StatementOfAccountFeatures;
using AgricultureManager.Module.Accounting.Models;
using AgricultureManager.Module.Accounting.Store.States;
using Fluxor;
using MediatR;
using Microsoft.AspNetCore.Components;
using Radzen.Blazor;

namespace AgricultureManager.Module.Accounting.Components
{
    public partial class StatementOfAccountList
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public IState<AccountState> AccountState { get; set; } = default!;

        private RadzenDataGrid<StatementOfAccountDocumentVm> _grid = default!;
        private ICollection<StatementOfAccountDocumentVm> _list = [];
        private bool _isLoading;
        private DateTime? _startDate;
        private DateTime? _endDate;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            AccountState.StateChanged += async (s, e) =>
            {
                await LoadDataAsync();
                await InvokeAsync(StateHasChanged);
            };
        }
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            _startDate = DateTime.Now.AddMonths(-5).Date;
            _endDate = DateTime.Now.Date;
            await LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            _isLoading = true;

            var response = await Mediator.Send(new GetStatementOfAccountListByDateCommand(_startDate, _endDate));
            _list = response.Data?.OrderByDescending(f => f.Year).ToList() ?? [];
            _isLoading = false;
            await InvokeAsync(StateHasChanged);
        }

        private async Task DeleteRow(StatementOfAccountDocumentVm item)
        {
            var response = await Mediator.Send(new RemoveStatementOfAccountCommand(item.Id));
            if (response.Success)
            {
                _list.Remove(item);
                await _grid.Reload();
            }
        }

        private async Task StartDateChanged(DateTime? startDate)
        {
            await LoadDataAsync();
        }
        private async Task EndDateChanged(DateTime? startDate)
        {
            await LoadDataAsync();
        }
    }
}
