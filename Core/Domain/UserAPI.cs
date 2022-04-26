using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class UserAPI
    {

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public ICollection<UserProfile> userProfiles { get; set; }

        public UserAPI()
        {
            userProfiles = new HashSet<UserProfile>();
        }
    }
}
