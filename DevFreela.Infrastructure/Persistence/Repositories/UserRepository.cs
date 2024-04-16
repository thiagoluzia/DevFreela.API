using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private readonly DevFreelaDbContext _contexto;

        
        public UserRepository(DevFreelaDbContext contexto)
        {
            _contexto = contexto;
        }


        public async Task<User> GetByEmailPasswordAsync(string email, string passwordHash)
        {
            var user = await _contexto.Users
                .SingleOrDefaultAsync(x => x.Email == email && x.Password ==  passwordHash);

            return user;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _contexto.Users.SingleOrDefaultAsync(x => x.Id == id);

            return user;
        }

        public async Task<int> AddUser(User user)
        {
            await _contexto.Users.AddAsync(user);
            await _contexto.SaveChangesAsync();

            return user.Id;
        }
    }
}
