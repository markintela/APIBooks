using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Shared.ModelViews.UserAPI
{
    public class NewUserAPI
    {
        public string Login { get; set; }
        public string Senha { get; set; }

        public ICollection<ReferenceUserProfile> UserProfiles { get; set; }
    }
}
