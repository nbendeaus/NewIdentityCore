using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewIdentityCore.Models
{
    public class Menus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int ProfilesId { get; set; }
        public int RoleId { get; set; }
    }
}
