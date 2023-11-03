using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        public const string SessionKeyLogin = "_login";
        public IActionResult OnGet()
        {
            string json = HttpContext.Session.GetString(SessionKeyLogin);
            if (json != null)
            {
                HttpContext.Session.Remove(SessionKeyLogin);
            }
            return RedirectToPage("/Index");
        }
    }
}
