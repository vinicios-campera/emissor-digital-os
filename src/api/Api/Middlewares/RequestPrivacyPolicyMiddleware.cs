using OrderService.Api.Controllers;
using OrderService.Application.Interfaces.Services;

namespace OrderService.Api.Middlewares
{
    public class RequestPrivacyPolicyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _userService;
        private readonly IEnumerable<EndpointDataSource> _endpointSources;

        private readonly string[] methodsOverrides =
            {
                $"{nameof(UserController).Replace("Controller", "")}.{nameof(IUserService.GetUser)}".ToLower(),
                $"{nameof(UserController).Replace("Controller", "")}.{nameof(IUserService.AcceptPrivacyPolicy)}".ToLower()
            };

        public RequestPrivacyPolicyMiddleware(RequestDelegate next, IUserService userService, IEnumerable<EndpointDataSource> endpointSources)
        {
            _next = next;
            _userService = userService;
            _endpointSources = endpointSources;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var methodCalled = $"{context.Request.RouteValues["controller"]}.{context.Request.RouteValues["action"]}".ToLower();
            if (context.User != null && context.User.Identity!.IsAuthenticated && !methodsOverrides.Contains(methodCalled))
            {
                var user = _userService.GetUserLogged();
                if (!user!.PrivacyPolicyAccepted)
                    throw new Exception("Privacy policy not accepted");
            }
            await _next(context);
        }
    }
}