using AgricultureManager.Core.Application.Features.CompanyFeatures;
using MediatR;
using Microsoft.AspNetCore.Components;

namespace AgricultureManager.CoreApp.Client.Pages
{
    public partial class Home
    {
        [Inject] public IMediator Mediator { get; set; } = default!;
        [Inject] public NavigationManager NavigationManager { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var response = await Mediator.Send(new GetCompanyCommand());
            if (!response.Success)
            {
                NavigationManager.NavigateTo("/company/create");
            }
        }
    }
}
