using FluentValidation;
using FluentValidation.AspNetCore;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Application.Interfaces.Services;
using OrderService.Infrastructure.Repositories.MongoDb;
using OrderService.Infrastructure.Services;
using OrderService.Infrastructure.Validations;
using Kernel.Net.Api.FluentValidation;
using Kernel.Net.Http.Interfaces;
using Kernel.Net.Http.User;

namespace OrderService.Api.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureDI(this IServiceCollection services) =>
            services
                .ConfigureRepositories()
                .ConfigureDependencies()
                .ConfigureServices();

        private static IServiceCollection ConfigureRepositories(this IServiceCollection services) =>
            services
                .AddSingleton<ILogRepository, LogRepository>()
                .AddSingleton<IUserRepository, UserRepository>()
                .AddSingleton<IProductRepository, ProductRepository>()
                .AddSingleton<IClientRepository, ClientRepository>()
                .AddSingleton<INotificationRepository, NotificationRepository>()
                .AddSingleton<IOrderRepository, OrderRepository>()
                .AddSingleton<IMessageRepository, MessageRepository>();

        private static IServiceCollection ConfigureServices(this IServiceCollection services) =>
            services
                .AddSingleton<ILocalizationService, LocalizationService>()
                .AddSingleton<ILogService, LogService>()
                .AddSingleton<IUserService, UserService>()
                .AddSingleton<IProductService, ProductService>()
                .AddSingleton<IClientService, ClientService>()
                .AddSingleton<INotificationService, NotificationService>()
                .AddSingleton<IOrderService, Infrastructure.Services.OrderService>()
                .AddSingleton<IHelperService, HelperService>();

        private static IServiceCollection ConfigureDependencies(this IServiceCollection services) =>
            services
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddSingleton<IUserAccessor, UserAccessor>();

        public static IServiceCollection ConfigureFluentValidation(this IServiceCollection services) =>
            services
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddValidatorsFromAssemblyContaining<ProductValidationInsert>()
                .AddTransient<IValidatorInterceptor, CustomErrorModelInterceptor>();
    }
}