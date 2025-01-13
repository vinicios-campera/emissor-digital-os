using OrderService.Domain.Entities;
using Kernel.Data.Repository.Interfaces;

namespace OrderService.Application.Interfaces.Repositories
{
    public interface IMessageRepository : IRepositoryBase<Message, Guid>
    {
    }
}