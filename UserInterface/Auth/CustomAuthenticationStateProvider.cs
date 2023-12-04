namespace UserInterface.Auth
{
	using Microsoft.AspNetCore.Components.Authorization;
	using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
	using System.Security.Claims;
	public class CustomAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly ProtectedSessionStorage _sessionStorage;
		private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
		public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
		{
			_sessionStorage = sessionStorage;
		}
		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			try
			{
				var userSessionStorateResult = await _sessionStorage.GetAsync<UserSession>("UserSession");
				var userSession = userSessionStorateResult.Success ? userSessionStorateResult.Value : null;
				if (userSession == null)
				{
					return await Task.FromResult(new AuthenticationState(_anonymous));
				}
				else
				{
					var claimPriciple = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
 new Claim(ClaimTypes.Name, userSession.FirstName),
 new Claim(ClaimTypes.Role, userSession.Role),
 new Claim("UserId",userSession.UserId)
 }, "CustomAuth"));
					return await Task.FromResult(new AuthenticationState(claimPriciple));
				}
			}
			catch
			{
				return await Task.FromResult(new AuthenticationState(_anonymous));
			}

		}
		public async Task UpdateAuthenticatinoState(UserSession userSession)
		{
			ClaimsPrincipal claimsPrincipal;
			if (userSession != null)
			{
				await _sessionStorage.SetAsync("UserSession", userSession);
				claimsPrincipal = new ClaimsPrincipal(
				new ClaimsIdentity(new List<Claim>
				{
 new Claim(ClaimTypes.Name,userSession.FirstName),
 new Claim(ClaimTypes.Role,userSession.Role)
				}));
			}
			else
			{
				await _sessionStorage.DeleteAsync("UserSession");
				claimsPrincipal = _anonymous;
			}
			NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
		}

	}
}
