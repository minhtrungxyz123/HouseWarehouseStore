using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Data.Repositories;
using Master.Webapp.ApiClient;
using Master.Webapp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Master.Webapp.Controllers
{
    public class LoginController : Controller
    {
        #region Fields

        private readonly IAdminApiClient _adminApiClient;
        private readonly IRepositoryEF<Admin> _adminRepository;

        public LoginController(IRepositoryEF<Admin> adminrepository,
            IAdminApiClient adminApiClient)
        {
            _adminRepository = adminrepository;
            _adminApiClient = adminApiClient;
        }

        #endregion Fields

        #region Method

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index([Bind] LoginViewModel user, string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var checkActive = await _adminApiClient.GetCheckActive(user.AccountName);
                if (checkActive != null && checkActive.Active)
                {
                    if (ValidateAdmin(user.AccountName, user.Password))
                    {
                        var users = _adminRepository.Get(a => a.Username == user.AccountName).SingleOrDefault();
                        if (users != null)
                        {
                            var userClaims = new List<Claim>()
                        {
                                new Claim("Username", users.Username),
                                new Claim("Id", users.Id),
                        };
                            var userIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var authProperties = new AuthenticationProperties
                            {
                                AllowRefresh = true,
                                IsPersistent = true
                            };
                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userIdentity), authProperties);
                            if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                               && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                            {
                                return Redirect(returnUrl);
                            }
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, " Username does not exist ");
                            return View(user);
                        }
                    }
                }
                else if(checkActive != null && !checkActive.Active)
                {
                    ModelState.AddModelError(string.Empty, "Tài khoản chưa được kích hoạt.");
                    return View(user);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tên đăng nhập hoặc mật khẩu không chính xác.");
                    return View(user);
                }
            }
            return View(user);
        }

        public IActionResult FalseLogin()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        #endregion Method

        #region Unitiels

        [AllowAnonymous]
        public bool ValidateAdmin(string username, string password)
        {
            var admin = _adminRepository.Get(a => a.Username.Equals(username)).SingleOrDefault();
            return admin != null && new PasswordHasher<Admin>().VerifyHashedPassword(new Admin(), admin.Password, password) == PasswordVerificationResult.Success;
        }

        #endregion Unitiels
    }
}