using Kernel.Data.AutoMapper.Extensions;
using Kernel.Data.MongoDb.Extensions;
using Kernel.Net.Api.Configurations;
using Kernel.Net.Api.Extensions;
using Kernel.Net.Firebase.Configurations;
using Kernel.Net.Firebase.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using OrderService.Api.Extensions;
using OrderService.Api.Middlewares;
using OrderService.Domain.Data;
using OrderService.Infrastructure.AutoMapper.Mappers;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FirebaseCloudMessaging>(builder.Configuration.GetSection("FirebaseCloudMessaging"));

builder.Services
    .AddControllers()
    .AddOData(options => options.Select().Filter().OrderBy().Expand().SetMaxTop(100).Count())
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => JsonSerializer.Deserialize<Kernel.Net.Api.FluentValidation.Error>(e.ErrorMessage));

            return new BadRequestObjectResult(errors);
        };
    });

builder.Services
    .AddEndpointsApiExplorer()
    .ConfigureApiVersioning(1, 0)
    .ConfigureFluentValidation()
    .Configure<RouteOptions>(options => options.LowercaseUrls = true)
    .AddSwaggerDocAndSecurity((opt) =>
    {
        opt.Title = builder.Configuration.GetValue<string>("SwaggerDocAndSecurity:Title");
        opt.Description = builder.Configuration.GetValue<string>("SwaggerDocAndSecurity:Description");
        opt.Mainteiner = builder.Configuration.GetValue<string>("SwaggerDocAndSecurity:Mainteiner");
        opt.TermsOfService = builder.Configuration.GetValue<string>("SwaggerDocAndSecurity:TermsOfService");
        opt.MainteinerEmail = builder.Configuration.GetValue<string>("SwaggerDocAndSecurity:MainteinerEmail");
        opt.Type = SwaggerDocAndSecurityType.JWT;
    })
    .AddAuthenticationJwt((opt) =>
    {
        opt.Authority = builder.Configuration.GetValue<string>("Jwt:Authority");
        opt.Issuer = new JwtIssuer { ValidIssuer = builder.Configuration.GetValue<string>("Jwt:ValidIssuer"), ValidateIssuer = true };
        opt.Audience = new JwtAudience { ValidAudience = builder.Configuration.GetValue<string>("Jwt:Audience"), ValidateAudience = false };
        //TODO::VERIFICAR PQ NAO PASSA QUANDO VALIDA O AUDIENCE
    })
    .UseMongoDb<OrderServiceDb>(builder.Configuration, "MongoDb")
    .ConfigureDI()
    .AddFirebaseCloudMessaging()
    .AddLazyResolution()
    .ConfigureAutoMapper(typeof(ClientMapper));

var app = builder.Build();

app.ConfigureExteptionHandler();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseCors(x =>
{
    x.AllowAnyMethod();
    x.AllowAnyOrigin();
    x.AllowAnyHeader();
});
app.EnableSwaggerUI((opt) =>
{
    opt.AppName = builder.Configuration.GetValue<string>("SwaggerUI:AppName");
});
app.UseMiddleware<RequestPrivacyPolicyMiddleware>();
app.Run();