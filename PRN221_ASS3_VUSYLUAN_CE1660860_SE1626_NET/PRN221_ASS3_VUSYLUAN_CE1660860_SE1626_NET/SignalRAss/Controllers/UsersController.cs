using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRAss.Models;
using System.Security.Claims;
using System.Security.Principal;

namespace SignalRAss.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IHubContext<SignalrServer> _signalRHub;

        public UsersController(ApplicationDBContext context, IHubContext<SignalrServer> signalRHub)
        {
            _context = context;
            _signalRHub = signalRHub;
        }
        public IActionResult Index()
        {
            return View();
        }

  
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(AppUser appUser)
        {
            var acc = _context.AppUsers.Where(a => a.Email == appUser.Email && a.Password == appUser.Password).FirstOrDefault();
            if (acc == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else
            {
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, acc.Email),
            new Claim("FullName", acc.FullName),
            new Claim(ClaimTypes.Email, acc.Email),
        };

                var claimsIdentity = new ClaimsIdentity(
                  claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {

                };

                HttpContext.SignInAsync(
                   CookieAuthenticationDefaults.AuthenticationScheme,
                   new ClaimsPrincipal(claimsIdentity),
                   authProperties);
                return RedirectToAction("Index", "Home");
            }
            ViewData["msg"] = "Username and Password combination is incorrect!";
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(AppUser appUser)
        {
            var acc = _context.AppUsers.FirstOrDefault(x => x.Email == appUser.Email);
                try
                {
                    if(acc != null)
                {
                    return RedirectToAction("Register", "Users");
                }
                else
                {
                    _context.AppUsers.Add(appUser);
                    _context.SaveChanges();
                    return RedirectToAction("Login", "Users");
                }
                   
                }
                catch (Exception ex)
                {
        
                    ModelState.AddModelError("", "Đã xảy ra lỗi trong quá trình đăng ký.");
                    return View(appUser);
                }
            
        
            return View(appUser);
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(
               CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Users");
        }
    }
}
