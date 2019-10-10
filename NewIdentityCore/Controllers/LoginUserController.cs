using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewIdentityCore.Data;
using NewIdentityCore.Models;
using NewIdentityCore.Todo;

namespace NewIdentityCore.Controllers
{
    public class LoginUserController : Controller
    {
        ApplicationDbContext _dataContext = new ApplicationDbContext(null);
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Authenticate([FromBody]LoginUser userDto)
        {
            var user = Authenticate(userDto.FirstName, userDto.Password);
            if (user == null)
                return Unauthorized();

           // //Role Master
           // List<Role> roles = new List<Role>()
           // {
           //     new Role{Id="1", Name="Admin",RoleDescription="Admin" },
           //     new Role{Id="2", Name="User",RoleDescription="User" },
           //     new Role{Id="3", Name="Super Admin",RoleDescription="Super Admin" },
           //     new Role{Id="4", Name="Initiater",RoleDescription="Initiater" },
           //     new Role{Id="5", Name="Executer",RoleDescription="Executer" },
           //     new Role{Id="6", Name="Closer",RoleDescription="Closer" }
           // };

           // //UsersInRole Master

           // List<UsersInRoles> usersInRoleList = new List<UsersInRoles>()
           // {
           //     new UsersInRoles{ Id=1, RoleId=1,UserId=1},
           //     new UsersInRoles{ Id=2, RoleId=2,UserId=1},
           //     new UsersInRoles{ Id=3, RoleId=3,UserId=1},
           //     new UsersInRoles{ Id=4, RoleId=4,UserId=1},
           // };

           // //Profiles master
           // List<Profiles> profiles = new List<Profiles>()
           // {
           //     new Profiles{ Id=1, ProfilesDescription="Admin" },
           //     new Profiles{ Id=2, ProfilesDescription="Masters" },
           //     new Profiles{ Id=3, ProfilesDescription="Transactions" },
           //     new Profiles{ Id=4, ProfilesDescription="Configurations" },
           //     new Profiles{ Id=5, ProfilesDescription="Reports" },
           // };

           // //Menus Master
           // List<Menus> menus = new List<Menus>()
           // {
           //     new Menus{Id=1,RoleId=1,ProfilesId=1,Action="ActionName11",Controller="ControllerName11"},
           //     new Menus{Id=2,RoleId=1,ProfilesId=1,Action="ActionName12",Controller="ControllerName21"},
           //     new Menus{Id=3,RoleId=1,ProfilesId=2,Action="ActionName21",Controller="ControllerName21"},
           //     new Menus{Id=4,RoleId=1,ProfilesId=3,Action="ActionName22",Controller="ControllerName22"},
           // };

           // //var tokenHandler = new JwtSecurityTokenHandler();
           // //var key = Encoding.ASCII.GetBytes(AppSettings.Secret);
            
           // List<Claim> obj = new List<Claim>();

           //// getting role names
           // var roleNamesList = (from Ur in usersInRoleList
           //          join ro in roles on Ur.RoleId equals Convert.ToInt32(ro.Id) //need to check ID - It is system generated
           //          select new
           //          {
           //              ro.RoleDescription,
           //          }).ToList();

           // foreach (var item in roleNamesList)
           // {
           //     obj.Add(new Claim(ClaimTypes.Role, item.RoleDescription));
           // }

           // var tokenDescriptor = new SecurityTokenDescriptor
           // {
           //     Subject = new ClaimsIdentity(obj),

           //     Expires = DateTime.UtcNow.AddDays(7),
           //     //SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
           // };

            //var token = tokenHandler.CreateToken(tokenDescriptor);
            //var tokenString = tokenHandler.WriteToken(token);

            // return basic user info (without password) and token to store client side
            return Ok(new
            {
                Id = user.Id,
                //Token = tokenString,
                //menusubmens= MenuandSubmenus,
                //submenus= rolebasedMenus
            });
        }

        public LoginUser Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _dataContext.loginUsers.SingleOrDefault(x => x.FirstName == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            ////////////////if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            ////////////////    return null;

            // authentication successful
            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}