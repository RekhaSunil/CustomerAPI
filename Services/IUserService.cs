using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Services
{
    public interface IUserService
    {
        bool ValidateCredentials(String username, String password);
    }
}
