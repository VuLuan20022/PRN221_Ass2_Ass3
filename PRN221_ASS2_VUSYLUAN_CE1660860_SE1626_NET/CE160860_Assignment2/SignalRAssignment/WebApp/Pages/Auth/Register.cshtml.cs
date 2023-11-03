using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.Auth
{
    public class RegisterModel : PageModel
    {
        private readonly AssDBContext context;
        [BindProperty]
        public Account account { get; set; }
        public RegisterModel(AssDBContext context)
        {
            this.context = context;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            try
            {
               
                var a = context.Accounts.FirstOrDefault(
                    x => x.UserName == account.UserName );
                if (a != null)
                {
                    return RedirectToPage();
                }
                context.Accounts.Add(account);
                context.SaveChanges();
                return RedirectToPage("/Auth/Login");
            }
            catch (Exception ex)
            {
                return RedirectToPage();
            }
        }
    }
}
