using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using admin.Services.Abstract;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using admin.Models;

namespace admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _uow;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork uow)
        {
            _logger = logger;
            _uow = uow;
        }
        public IActionResult Index()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("index");
            }
            else
            {
                return View("login");
            }
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }   
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]//Güvenlik Açısından önemli
        public async Task<IActionResult> Login(string username, string password, string returnUrl)
        {
            // && => Ve
            // ||  => Ya da
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            Admin admin = _uow.Admin.GetAdmin(username, password);
            Member member = _uow.Member.GetMember(username, password);
            if (admin == null && member == null)
            {
                return View("Login");
            }
            else if (member != null)
            {
                Claim[] claimsMember = new[] {
                    new Claim("id", member.Id.ToString()),
                    new Claim("name",  member.Name),
                    new Claim("image",   member.Image),
                    new Claim("role", "member"),
                    new Claim("mail", member.Mail),
                };
                ClaimsIdentity identityMember = new ClaimsIdentity(claimsMember, member.Name);
                ClaimsPrincipal principalMember = new ClaimsPrincipal(identityMember);
                identityMember.AddClaim(new Claim(ClaimTypes.Role, "member"));
                _uow.Member.Update(member);


                await HttpContext.SignInAsync(principalMember, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddYears(1)
                });
                if (returnUrl != null)
                {
                    return Redirect(returnUrl ?? "/");
                }
                return RedirectToAction("Index");
            }
            else if(admin != null)
            {
                Claim[] claims = new[] {
                    new Claim("id", admin.Id.ToString()),
                    new Claim("name",  admin.Name),
                    new Claim("image",  admin.Image),
                    new Claim("isAdmin", admin.IsAdmin.ToString()),
                    new Claim("role", admin.Role),
                    new Claim("mail", admin.Email),
                };
                ClaimsIdentity identity = new ClaimsIdentity(claims, admin.Name);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                if (admin.Role.Contains("editor"))
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "editor"));
                }
                else
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                }
                _uow.Admin.Update(admin);


                await HttpContext.SignInAsync(principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddYears(1)
                });
                if (returnUrl != null)
                {
                    return Redirect(returnUrl ?? "/");
                }
                return RedirectToAction("Index");
            }
            
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(Member entity, string PasswordAgain)
        {
            if (ModelState.IsValid)
            {
                if (entity.TermConfirm == false)
                {
                    ViewBag.AlertTerm = "Onaylamadan devam edemeyiz.";
                    return View();
                }
                else if (entity.Password == PasswordAgain)
                {
                    if (entity.Image == null)
                    {
                        entity.Image = "default-profile.png";
                    }
                    _uow.Member.Add(entity);
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Alert = "Lütfen şifrenizi doğru giriniz.";
                    return View();
                }
            }
            return View(entity);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
