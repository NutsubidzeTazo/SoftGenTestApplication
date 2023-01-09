using SoftGen.Domain.Entities;
using SoftGen.Repository.Interfaces;
using SoftGen.Services.Enums;
using SoftGen.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftGen.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> _userBaseRepository;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IBaseRepository<User> userBaseRepository)
        {
            _userRepository = userRepository;
            _userBaseRepository = userBaseRepository;
        }

        public async Task<User> GetUserAsync(User user)
        {

            return await _userRepository.GetUserAsync(user);
        }

        public async Task<ResponseEnum> RegisterUserAsync(User user)
        {
            var checkUser = await _userRepository.GetUserAsync(user);
            if (checkUser == null)
            {
                await _userRepository.RegisterUserAsync(user);
                return ResponseEnum.Success;
            }
            else
            {
                return ResponseEnum.Failed;
            }
        }
    }
}
