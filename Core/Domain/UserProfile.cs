using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public ICollection<UserAPI> usersAPI { get; set; }
    }
}
