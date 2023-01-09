using SoftGen.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftGen.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(User user);
        Task RegisterUserAsync(User user);
    }
}
