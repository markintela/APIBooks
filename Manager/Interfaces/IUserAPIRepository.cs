using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Interfaces
{
    public interface IUserAPIRepository
    {

        Task<IEnumerable<UserAPI>> GetAsync();

        Task<UserAPI> GetAsync(string login);

        Task<UserAPI> InsertAsync(UserAPI userAPI);

        Task<UserAPI> UpdateAsync(UserAPI userAPI);
    }
}
