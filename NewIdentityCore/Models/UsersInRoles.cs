using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewIdentityCore.Models
{
    public class UsersInRoles
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        //public virtual LoginUser User { get; set; }
        //public virtual Role Role { get; set; }
    }
}
