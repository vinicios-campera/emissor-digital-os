using Kernel.Data.Repository.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces.Repositories
{
    public interface IClientRepository : IRepositoryBase<Client, Guid>
    {
    }
}