using AgricultureManager.Core.Application.Features.IdentityFeatures;
using AgricultureManager.Core.Application.Shared.Models.Identity;
using Blazored.LocalStorage;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace AgricultureManager.Core.Application.Services
{
    public class AmAuthenticationStateProvider(ILocalStorageService localStorage, IMediator mediator) : AuthenticationStateProvider
    {
        const string UserTokenKey = "UserToken";
        private UserVm? _currentUser;
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (_currentUser is null)
            {
                try
                {
                    var userToken = await localStorage.GetItemAsync<UserToken>(UserTokenKey);
                    if (userToken is not null && userToken.Expiration > DateTime.UtcNow && userToken.Expiration <= DateTime.UtcNow.AddDays(14))
                    {
                        var response = await mediator.Send(new GetUserCommand(userToken.Username));
                        if (response.Success && response.Data is not null)
                        {
                            _currentUser = response.Data;
                        }
                        else
                        {
                            _currentUser = null;
                        }
                    }
                }
                catch (Exception)
                {
                    _currentUser = null;
                }
            }
#if DEBUG
            _currentUser = new UserVm { Firstname = "Mock", Lastname = "User" };
#endif
            return CreateAuthenticationState(_currentUser);
        }

        public async Task<UserVm> Login(string username, string password)
        {
            var response = await mediator.Send(new AuthenticateUserCommand(username, password));
            if (!response.Success || response.Data is null)
            {
                return new UserVm
                {
                    LogonMessage = response.Message ?? "Fehler bei der Anmeldung",
                };
            }

            var userToken = new UserToken
            {
                Username = username,
                Expiration = DateTime.UtcNow.AddDays(14)
            };
            await localStorage.SetItemAsync(UserTokenKey, userToken);
            _currentUser = response.Data;

            Notify();
            return _currentUser;
        }

        public async Task Logout()
        {
            if (_currentUser is null) return;

            _currentUser = null;
            await localStorage.RemoveItemAsync(UserTokenKey);
            Notify();
        }

        public void Notify()
        {
            var state = CreateAuthenticationState(_currentUser);
            var task = Task.FromResult(state);
            NotifyAuthenticationStateChanged(task);
        }

        private static AuthenticationState CreateAuthenticationState(UserVm? user)
        {
            if (user is null)
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var name = string.IsNullOrWhiteSpace(user.Firstname) && string.IsNullOrWhiteSpace(user.Lastname)
                ? user.UserName
                : $"{user.Firstname} {user.Lastname}";

            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, name),
                new(ClaimTypes.NameIdentifier, user.UserName),
                new(ClaimTypes.Role, "User")
            };

            var claimsIdentity = new ClaimsIdentity(claims, "login");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            return new AuthenticationState(claimsPrincipal);
        }
    }
}
