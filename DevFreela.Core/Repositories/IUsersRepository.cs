using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories
{
    public interface IUsersRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailPasswordAsync(string email, string passwordHash);
        Task<int> AddUser(User user);
    }
}
