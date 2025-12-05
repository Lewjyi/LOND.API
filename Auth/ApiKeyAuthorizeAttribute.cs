using Microsoft.AspNetCore.Authorization;

namespace LOND.API.Auth
{
    public class ApiKeyAuthorizeAttribute : AuthorizeAttribute
    {
        public ApiKeyAuthorizeAttribute() : base("ApiKey")
        {
        }
    }
}
