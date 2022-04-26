using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Interfaces
{
    public interface IJWTService
    {
        string CreateToken(UserAPI userAPI);
    }
}
