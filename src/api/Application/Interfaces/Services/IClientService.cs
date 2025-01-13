using OrderService.Domain.Dto.Insert;
using OrderService.Domain.Dto.Response;
using OrderService.Domain.Dto.Update;
using OrderService.Domain.Entities;
using System.Linq.Expressions;

namespace OrderService.Application.Interfaces.Services
{
    public interface IClientService
    {
        bool AddClient(ClientInsert payload);

        bool UpdateClient(ClientUpdate payload);

        IEnumerable<ClientResponse>? GetClients(Expression<Func<Client, bool>> expression);

        bool DeleteClient(Guid id);

        ClientResponse? GetClientById(Guid id);
    }
}