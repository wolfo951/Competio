using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System.Text.Encodings.Web;

namespace API_Tournament.Authorization
{
    public class AuthHandler : AuthenticationHandler<AuthSchemeOptions>
    {
        public AuthHandler(IOptionsMonitor<AuthSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var ticket = new AuthenticationTicket(new System.Security.Claims.ClaimsPrincipal(), "");
            if(!this.Request.Headers.ContainsKey(HeaderNames.Authorization))
            {
                return Task.FromResult(AuthenticateResult.Fail("Header not found"));
            }

            var authorization = this.Request.Headers[HeaderNames.Authorization];

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }

    public class AuthSchemeOptions
        : AuthenticationSchemeOptions
    { }
}
