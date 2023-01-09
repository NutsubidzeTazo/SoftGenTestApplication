using SoftGen.Domain.Entities;
using SoftGen.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftGen.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserAsync(User user);
        Task<ResponseEnum> RegisterUserAsync(User user);
    }
}
