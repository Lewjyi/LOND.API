using LOND.API.Auth;
using LOND.API.Database;
using LOND.API.Interfaces;
using LOND.API.Models;
using LOND.API.Models.Config;
using LOND.API.Repositories;
using LOND.API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "API Key Authorization header using the ApiKey scheme. Example: \"ApiKey: my_api_key\"",
        In = ParameterLocation.Header,
        Name = "ApiKey",
        Type = SecuritySchemeType.ApiKey
    });

    s.OperationFilter<SecurityRequirementsOperationFilter>();
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContext<LondDbContext>(o => o.UseSqlServer(connectionString));
RegisterServices(builder, builder.Services);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger((Swashbuckle.AspNetCore.Swagger.SwaggerOptions options) => { });
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Optional: serve Swagger UI at root
    });
}
app.UseCors("AllowSpecificOrigin");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();

return;
void RegisterServices(WebApplicationBuilder b, IServiceCollection services)
{

    services.AddScoped<IUserService, UserService>();
    services.AddScoped<ILondRepository<LondUser, int>, UserRepository>();



    services.Configure<AuthConfig>(builder.Configuration.GetSection(AuthConfig.ConfigSection));
    services.AddAuthorizationBuilder()
        .AddPolicy("ApiKey", policyBuilder =>
        {
            policyBuilder.AuthenticationSchemes.Add("ApiKey");
            policyBuilder.RequireAuthenticatedUser();
        });
    services.AddAuthentication()
        .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", options => { });
    services.AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin",
            builder => builder.WithOrigins("http://localhost:4200")
                              .AllowAnyHeader()
                              .AllowAnyMethod());
    });


}