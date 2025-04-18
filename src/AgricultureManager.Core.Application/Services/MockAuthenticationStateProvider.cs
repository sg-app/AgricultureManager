using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AgricultureManager.Core.Application.Services
{
    public class MockAuthenticationStateProvider : AuthenticationStateProvider
    {
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity([new Claim(ClaimTypes.Name, "Debug")], "Debugging authentication type");
            var user = new ClaimsPrincipal(identity);
            return Task.FromResult(new AuthenticationState(user));
        }
    }
}
