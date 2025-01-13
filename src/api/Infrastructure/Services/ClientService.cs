using AutoMapper;
using OrderService.Application.Interfaces.Repositories;
using OrderService.Application.Interfaces.Services;
using OrderService.Domain.Dto.Insert;
using OrderService.Domain.Dto.Response;
using OrderService.Domain.Dto.Update;
using OrderService.Domain.Entities;
using OrderService.Infrastructure.Constants;
using System.Linq.Expressions;
using Kernel.Data.AutoMapper.Extensions;
using Kernel.Net.Http.Interfaces;
using Kernel.Toolkit.Extensions;

namespace OrderService.Infrastructure.Services
{
    public class ClientService : IClientService
    {
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;
        private readonly IClientRepository _clientRepository;
        private readonly ILocalizationService _localizationService;
        private readonly Expression<Func<Client, bool>> _defaultExpression;

        public ClientService(IMapper mapper, IUserAccessor userAccessor, IClientRepository clientRepository, ILocalizationService localizationService)
        {
            _mapper = mapper;
            _userAccessor = userAccessor;
            _clientRepository = clientRepository;
            _localizationService = localizationService;
            _defaultExpression = x => x.UserEmail == userAccessor.GetUserEmail();
        }

        public bool AddClient(ClientInsert payload) => _clientRepository.Insert(_mapper.Map<Client>(payload, GetMapperUserIdAndCity(payload.Cep!)));

        public bool DeleteClient(Guid id) => _clientRepository.Delete(id);

        public IEnumerable<ClientResponse>? GetClients(Expression<Func<Client, bool>> expression)
            => _mapper.Map<IEnumerable<ClientResponse>>(_clientRepository.Find(_defaultExpression.And(expression)));

        public bool UpdateClient(ClientUpdate payload)
        {
            var existing = _clientRepository.FindById(payload.Id).ValidateIsNull();
            var mapped = _mapper.Map(payload, existing, GetMapperUserIdAndCity(payload.Cep!));
            return _clientRepository.Update(mapped!);
        }

        public ClientResponse? GetClientById(Guid id)
            => _mapper.Map<ClientResponse>(_clientRepository.FindById(id).ValidateIsNull());

        private Action<IMappingOperationOptions> GetMapperUserIdAndCity(string cep)
            => new Dictionary<string, object?>
            {
                {
                    AutomapperConstants.PARAM_USER_EMAIL,
                    _userAccessor.GetUserEmail()
                },
                {
                    AutomapperConstants.PARAM_CITY,
                    _localizationService.GetCityByCep(cep)
                }
            }.GetParams();
    }
}