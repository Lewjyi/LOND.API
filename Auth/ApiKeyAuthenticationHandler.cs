using System.Security.Claims;
using System.Text.Encodings.Web;
using Azure.Core;
using LOND.API.Models.Config;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Swashbuckle.Swagger.Model;

namespace LOND.API.Auth
{
    public class ApiKeyAuthenticationHandler(
     IOptionsMonitor<AuthenticationSchemeOptions> options,
     ILoggerFactory logger,
     UrlEncoder encoder,
     ISystemClock clock,
     IOptions<AuthConfig> authConfig)
     : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder, clock)
    {
        private readonly AuthConfig authConfig = authConfig.Value;
        private readonly ILogger<ApiKeyAuthenticationHandler> logger = logger.CreateLogger<ApiKeyAuthenticationHandler>();

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Allow access to swagger
            if (Request.Path.Value is "/swagger/index.html" or "/swagger/v1/swagger.json")
            {
                var claims = new[] { new Claim(ClaimTypes.Name, "Test") };
                var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Tokens"));
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }

            if (!Request.Headers.TryGetValue("Apikey", out var potentialApiKey))
            {
                logger.LogError("API Key was not found");
                return AuthenticateResult.Fail("API Key was not found");
            }
            var apiKey = potentialApiKey.ToString();

            if (apiKey.Equals(authConfig.ApiKey))
            {
                var claims = new[] { new Claim(ClaimTypes.Name, "Test") };
                var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, "Tokens"));
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }

            logger.LogError("Invalid API Key");
            return AuthenticateResult.Fail("Invalid API Key");
        }
    }
}
