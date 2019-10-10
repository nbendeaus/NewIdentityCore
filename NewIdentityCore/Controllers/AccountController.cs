using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewIdentityCore.Data;
using NewIdentityCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NewIdentityCore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<LoginUser> userManger;
        private readonly SignInManager<LoginUser> signInManager;
        private readonly ApplicationDbContext applicationDbContext;

        public AccountController(UserManager<LoginUser> userManger, SignInManager<LoginUser> signInManager, ApplicationDbContext applicationDbContext)
        {
            this.userManger = userManger;
            this.signInManager = signInManager;
            this.applicationDbContext = applicationDbContext;
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, LoginUser createUser)
        {
            if (ModelState.IsValid)
            {
                var user = new LoginUser { UserName = registerViewModel.UserName, Email = registerViewModel.Email };
                var result = await userManger.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(registerViewModel);
        }
        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string Email)
        {
            var user = await userManger.FindByEmailAsync(Email);
            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {Email} is already in use");
            }
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            loginViewModel.Email = "demo";
            loginViewModel.Password = "Audree@123";
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, false);
                signInManager.cl

                if (result.Succeeded)
                {
                    #region Binding roles,menus and submenus to the user
                    //Role Master
                    List<Role> roles = new List<Role>()
                    {
                        new Role{Id="1", Name="Admin",RoleDescription="Admin" },
                        new Role{Id="2", Name="User",RoleDescription="User" },
                        new Role{Id="3", Name="Super Admin",RoleDescription="Super Admin" },
                        new Role{Id="4", Name="Initiater",RoleDescription="Initiater" },
                        new Role{Id="5", Name="Executer",RoleDescription="Executer" },
                        new Role{Id="6", Name="Closer",RoleDescription="Closer" }
                    };

                    //UsersInRole Master

                    List<UsersInRoles> usersInRoleList = new List<UsersInRoles>()
                    {
                        new UsersInRoles{ Id=1, RoleId=1,UserId=1},
                        new UsersInRoles{ Id=2, RoleId=2,UserId=1},
                        new UsersInRoles{ Id=3, RoleId=3,UserId=1},
                        new UsersInRoles{ Id=4, RoleId=4,UserId=1},
                    };

                    //Profiles master
                    List<Profiles> profiles = new List<Profiles>()
                    {
                        new Profiles{ Id=1, ProfilesDescription="Admin" },
                        new Profiles{ Id=2, ProfilesDescription="Masters" },
                        new Profiles{ Id=3, ProfilesDescription="Transactions" },
                        new Profiles{ Id=4, ProfilesDescription="Configurations" },
                        new Profiles{ Id=5, ProfilesDescription="Reports" },
                    };

                    //Menus Master
                    List<Menus> menus = new List<Menus>()
                    {
                        new Menus{Id=1,RoleId=1,ProfilesId=1,Action="ActionName11",Controller="ControllerName11"},
                        new Menus{Id=2,RoleId=1,ProfilesId=1,Action="ActionName12",Controller="ControllerName21"},
                        new Menus{Id=3,RoleId=1,ProfilesId=2,Action="ActionName21",Controller="ControllerName21"},
                        new Menus{Id=4,RoleId=1,ProfilesId=3,Action="ActionName22",Controller="ControllerName22"},
                    };

                    List<Claim> obj = new List<Claim>();

                    // getting role names
                    #region getting role names
                    var roleNamesList = (from Ur in usersInRoleList
                                         join ro in roles on Ur.RoleId equals Convert.ToInt32(ro.Id) //need to check ID - It is system generated
                                         select new
                                         {
                                             ro.RoleDescription,
                                         }).ToList();
                    #endregion

                    //To bind Roles to Claim
                    foreach (var item in roleNamesList)
                    {
                        obj.Add(new Claim(ClaimTypes.Role, item.RoleDescription));
                    }

                    #region Token Implementation
                    //var tokenHandler = new JwtSecurityTokenHandler();
                    //var key = Encoding.ASCII.GetBytes(AppSettings.Secret);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(obj),
                        Expires = DateTime.UtcNow.AddDays(7),
                        //SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };
                    #endregion
                 #endregion
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid User Name or Password");
            }
            return View(loginViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public IActionResult ChangePassWord()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePassword)
        {
            var user = await userManger.GetUserAsync(User);
            var passwordHash = userManger.PasswordHasher.HashPassword(user, changePassword.Password);
            var result = await userManger.RemovePasswordAsync(user);
            if (result.Succeeded)
            {
                await userManger.AddPasswordAsync(user, changePassword.Password);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}