using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatingAppAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Entities.User user);
    }
}
