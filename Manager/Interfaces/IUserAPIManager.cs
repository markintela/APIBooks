using Core.Domain;
using Core.Shared.ModelViews.UserAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Interfaces
{
    public interface IUserAPIManager
    {

        Task<IEnumerable<UserAPIView>> GetAsync();

        Task<UserAPIView> GetAsync(string login);

        Task<UserAPIView> InsertAsync(NewUserAPI userAPI);

        Task<UserAPIView> UpdateUserAPIAsync(UserAPI userAPI);

        Task<UserAPILogged> ValidaUserAPIEGeraTokenAsync(UserAPI userAPI);
    }
}
