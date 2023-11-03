using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.Accounts
{
    public class IndexModel : PageModel
    {
        private readonly WebApp.Data.AssDBContext _context;
        public const string SessionKeyLogin = "_login";
        public Account acc { get; set; }
        public Customer cus { get; set; }
        public IndexModel(WebApp.Data.AssDBContext context)
        {
            _context = context;
        }

        public IList<Account> Account { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyLogin)))
            {
                // If session doesn't exist, redirect to login page
                return RedirectToPage("/Auth/Login");
            }

            string json = HttpContext.Session.GetString(SessionKeyLogin);
            try
            {
                acc = JsonConvert.DeserializeObject<Account>(json);
            }
            catch (Exception ex)
            {
                cus = JsonConvert.DeserializeObject<Customer>(json);
            }

            if (_context.Accounts != null)
            {
                Account = await _context.Accounts.ToListAsync();
            }

            return Page();
        }

    }
}


