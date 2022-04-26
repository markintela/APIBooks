using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Shared.ModelViews.UserAPI
{
    public class UserAPIView
    {
        public string Login { get; set; }
        public ICollection<UserProfileView> userProfiles { get; set; }
    }
}
