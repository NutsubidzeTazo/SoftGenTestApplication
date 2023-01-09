using SoftGen.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftGen.Services.Interfaces
{
    public interface ITokenService
    {
        string BuildToken(string key, string issuer, User user);
        public bool IsTokenValid(string key, string issuer, string token);
    }
}
