using HouseWarehouse.Webapp.ApiClient;
using HouseWarehouse.Webapp.Models;
using HouseWarehouseStore.Data.Entities;
using HouseWarehouseStore.Data.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HouseWarehouse.Webapp.Controllers
{
    public class LoginController : Controller
    {
        #region Fields

        private readonly IMemberApiClient _memberApiClient;
        private readonly IRepositoryEF<Member> _memberRepositoryEF;

        public LoginController(IRepositoryEF<Member> memberRepositoryEF,
            IMemberApiClient memberApiClient)
        {
            _memberRepositoryEF = memberRepositoryEF;
            _memberApiClient = memberApiClient;
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
                var checkActive = await _memberApiClient.GetCheckActive(user.AccountName);
                if (checkActive != null && checkActive.Active)
                {
                    if (ValidateAdmin(user.AccountName, user.Password))
                    {
                        var users = _memberRepositoryEF.Get(a => a.Email == user.AccountName).SingleOrDefault();
                        if (users != null)
                        {
                            var userClaims = new List<Claim>()
                        {
                                new Claim("Email", users.Email),
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
                            ModelState.AddModelError(string.Empty, " Email does not exist ");
                            return View(user);
                        }
                    }
                }
                else if (checkActive != null && !checkActive.Active)
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

        #endregion Method

        #region Unitiels

        [AllowAnonymous]
        public bool ValidateAdmin(string username, string password)
        {
            var admin = _memberRepositoryEF.Get(a => a.Email.Equals(username)).SingleOrDefault();
            return admin != null && new PasswordHasher<Member>().VerifyHashedPassword(new Member(), admin.Password, password) == PasswordVerificationResult.Success;
        }

        #endregion Unitiels
    }
}