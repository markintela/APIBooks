using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Shared.ModelViews.UserAPI
{
    public class UserAPILogged
    {
        public string Login { get; set; }
        public ICollection<UserProfileView> UserProfiles { get; set; }
        public string Token { get; set; }
    }
}
