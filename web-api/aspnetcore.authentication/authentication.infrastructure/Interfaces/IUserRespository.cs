using authentication.domain.Entities;
using System.Threading.Tasks;

namespace authentication.infrastructure.Interfaces
{
    public interface IUserRespository
    {
        Task<int> Add(User user);
        Task<User> GetByEmail(string email);
        Task<bool> CheckAlreadyExist(string email);
        Task<User> RegisterAccess(User user);
    }
}
