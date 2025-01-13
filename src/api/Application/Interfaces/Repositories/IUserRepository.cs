using Kernel.Data.Repository.Interfaces;
using OrderService.Domain.Entities;

namespace OrderService.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepositoryBase<User, Guid>
    {
        User? FindByEmail(string email);
    }
}