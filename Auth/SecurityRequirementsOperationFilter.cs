using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
namespace LOND.API.Auth;    

internal class SecurityRequirementsOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context != null && operation != null)
        {
            // AuthenticationSchemes map to scopes
            // for class level authentication schemes
            var classLevel = context.MethodInfo.DeclaringType
                    .GetCustomAttributes(true)
                    .OfType<ApiKeyAuthorizeAttribute>()
                    .Any();

            //  for method level authentication scheme
            var methodLevel = context.MethodInfo
                    .GetCustomAttributes(true)
                    .OfType<ApiKeyAuthorizeAttribute>()
                    .Any();

            if (classLevel || methodLevel )
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });

                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
                            },
                            new[] { "DemoSwaggerDifferentAuthScheme" }
                        }
                    }
                };
            }
        }
    }
}