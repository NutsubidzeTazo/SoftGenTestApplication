using Microsoft.EntityFrameworkCore;
using SoftGen.Domain.Entities;
using SoftGen.Repository.Context;
using SoftGen.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftGen.Repository.Repositories
{
    public class UserRepository : BaseRepository<User>,IUserRepository
    {
        private readonly SoftGenApplicationDbContext _context;
        public UserRepository(SoftGenApplicationDbContext context) :base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(User user)
        {
            return await _context.Users.Where(x => x.Username == user.Username && x.Password == user.Password).FirstOrDefaultAsync();

        }
        public async Task RegisterUserAsync(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

        }

        public override Task<IEnumerable<User>> SearchAsync(string? searchString)
        {
            throw new NotImplementedException();
        }
    }
}
