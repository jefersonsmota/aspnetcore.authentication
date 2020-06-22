using authentication.domain.Entities;
using authentication.infrastructure.Context;
using authentication.infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace authentication.infrastructure.Repositories
{
    public class UserRepository : IUserRespository
    {
        private readonly AuthDbContext _context;
        public UserRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(User user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.AsNoTracking().Include(x => x.Phones).FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<bool> CheckAlreadyExist(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email.ToLowerInvariant() == email.ToLowerInvariant());
        }
        public async Task<User> RegisterAccess(User user)
        {
            _context.Users.Attach(user);
            _context.Entry(user).Property(x => x.LastLogin).IsModified = true;
            
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
