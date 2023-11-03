using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.Auth
{
    public class LoginModel : PageModel
    {
        [BindProperty(Name = "mess", SupportsGet = true)]
        public string mess { get; set; }

        public const string SessionKeyLogin = "_login";
        private readonly AssDBContext _context;

        public LoginModel(AssDBContext context)
        {
            _context = context;
        }

        public void OnGet(string mess)
        {
            this.mess = mess;   
        }

        public IActionResult OnPost(string userName, string password, string contactName)
        {
            var listAcc = _context.Accounts.ToList();
            var listCus = _context.Customers.ToList();

            userName = string.IsNullOrWhiteSpace(userName) ? "" : userName;
            password = string.IsNullOrWhiteSpace(password) ? "" : password;

            Account acc = listAcc.FirstOrDefault(x => x.UserName.ToLower().Equals(userName.ToLower()) &&
                x.Password.Equals(password));
            if (acc == null)
            {
                Customer cus = listCus.FirstOrDefault(x => x.Phone.ToLower().Equals(userName.ToLower())
                    && x.Password.Equals(password));
                if (cus != null)
                {
                    string json = JsonConvert.SerializeObject(cus);
                    if (HttpContext.Session != null)
                    {
                        HttpContext.Session.SetString(SessionKeyLogin, json);
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        // Handle the case where HttpContext.Session is null
                        return RedirectToPage("/Error");
                    }
                }
                return RedirectToPage("/Auth/Login", new { mess = "login fail" });
            }
            else
            {
                string json = JsonConvert.SerializeObject(acc);
                if (HttpContext.Session != null)
                {
                    HttpContext.Session.SetString(SessionKeyLogin, json);
                    return RedirectToPage("/Index");
                }
                else
                {
                    // Handle the case where HttpContext.Session is null
                    return RedirectToPage("/Error");
                }
            }
        }

    }
}
