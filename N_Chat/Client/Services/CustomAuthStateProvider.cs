using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace N_Chat.Client.Services;

public class CustomAuthStateProvider: AuthenticationStateProvider
{
    private ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return new AuthenticationState(claimsPrincipal);
    }
}