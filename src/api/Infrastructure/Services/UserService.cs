using AutoMapper;
using Kernel.Net.Http.Interfaces;
using Kernel.Toolkit.Extensions;
using Microsoft.Extensions.Configuration;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Application.Interfaces.Services;
using OrderService.Domain.Dto.Response;
using OrderService.Domain.Dto.Update;
using OrderService.Domain.Entities;

namespace OrderService.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly IUserRepository _userRepository;
        private readonly ILogService _logService;
        private readonly Lazy<IClientService> _clientService;
        private readonly Lazy<IOrderService> _orderService;
        private readonly Lazy<IProductService> _productService;
        private readonly Lazy<INotificationService> _notificationService;
        private readonly IConfiguration _configuration;

        public UserService(IMapper mapper,
            IUserAccessor userAccessor,
            IUserRepository userRepository,
            ILogService logService,
            Lazy<IClientService> clientService,
            Lazy<IOrderService> orderService,
            Lazy<IProductService> productService,
            Lazy<INotificationService> notificationService, 
            IConfiguration configuration)
        {
            _mapper = mapper;
            _userAccessor = userAccessor;
            _userRepository = userRepository;
            _logService = logService;
            _clientService = clientService;
            _orderService = orderService;
            _productService = productService;
            _notificationService = notificationService;
            _configuration = configuration;
        }

        public bool AcceptPrivacyPolicy()
        {
            var user = _userRepository.FindByEmail(_userAccessor.GetUserEmail()!).ValidateIsNull();
            if (!user!.PrivacyPolicyAccepted)
            {
                user!.PrivacyPolicyAccepted = true;
                user.SetModifiedDate();
                var result = _userRepository.Update(user);

                if (result)
                    _logService.InserNewLog($"User '{user.Id}' accepted privacy policy");

                return result;
            }

            return user!.PrivacyPolicyAccepted;
        }

        public UserResponse? GetUserLogged()
            => _mapper.Map<UserResponse>(_userRepository.FindByEmail(_userAccessor.GetUserEmail()!).ValidateIsNull());

        public User? GetUserDatabaseLogged()
            => _userRepository.FindByEmail(_userAccessor.GetUserEmail()!).ValidateIsNull();

        public UserResponse? GetUserById(Guid id)
            => _mapper.Map<UserResponse>(_userRepository.FindById(id).ValidateIsNull());

        public UserResponse? GetUser()
        {
            var user = _userRepository.FindByEmail(_userAccessor.GetUserEmail()!);
            if (user == null)
            {
                user = new()
                {
                    Name = _userAccessor.GetContext().User.FindFirst("name")?.Value,
                    Email = _userAccessor.GetUserEmail(),
                    Picture = getImage(_userAccessor.GetContext().User.FindFirst("picture")?.Value),
                    Roles = new List<string> { "default" }
                };
                user.SetInsertedDate();
                user.SetILastLoginDate();
                _userRepository.Insert(user);
            }
            else
            {
                user.SetILastLoginDate();
                _userRepository.Update(user);
            }

            var userMapped = _mapper.Map<UserResponse>(user);
            var notifications = _notificationService.Value.GetNotifications(x => true, true).OrderByDescending(x => x.Inserted).TakeLast(5);
            userMapped.Notifications = _mapper.Map<List<UserNotificationResponse>>(notifications);
            userMapped.PictureUrl = $"{_configuration.GetValue<string>("Urls:BaseUserPicture")}/{userMapped.Id}";
            return userMapped;
        }

        public bool UpdateUser(UserUpdate payload)
        {
            var user = _userRepository.FindById(payload.Id).ValidateIsNull();
            var data = _mapper.Map(payload, user);
            return _userRepository.Update(data);
        }

        public bool UpdatePushNotificationToken(string token)
        {
            var user = _userRepository.FindByEmail(_userAccessor.GetUserEmail()!).ValidateIsNull();
            user!.PushNotificationToken = token;
            user.SetModifiedDate();
            return _userRepository.Update(user);
        }

        public bool DeleteAccount()
        {
            var clients = _clientService.Value.GetClients(x => true);
            var orders = _orderService.Value.GetOrders(x => true);
            var items = _productService.Value.GetProducts(x => true);
            var users = _userRepository.Find(x => true && x.Email == _userAccessor.GetUserEmail());

            foreach (var _client in clients!)
                _clientService.Value.DeleteClient(_client.Id);

            foreach (var order in orders!)
                _orderService.Value.DeleteOrder(order.Id);

            foreach (var item in items!)
                _productService.Value.DeleteProduct(item.Id);

            foreach (var user in users!)
                _userRepository.Delete(user.Id);

            return true;
        }

        public bool UpdateAddPictureInOrder(bool insert)
        {
            var user = _userRepository.FindByEmail(_userAccessor.GetUserEmail()!).ValidateIsNull();
            user!.AddPictureInOrder = insert;
            user.SetModifiedDate();
            return _userRepository.Update(user);
        }

        public byte[]? GetPictureByUserId(Guid id)
        {
            var user = _userRepository.FindById(id);
            if (user == null)
                return null;

            return user.Picture;
        }

        private byte[]? getImage(string? imageUrl)
        {
            if (imageUrl == null)
                return null;

            using var httpClient = new HttpClient();
            var response = httpClient.GetAsync(imageUrl).Result;
            var content = response.Content.ReadAsByteArrayAsync().Result;
            return content;
        }
    }
}