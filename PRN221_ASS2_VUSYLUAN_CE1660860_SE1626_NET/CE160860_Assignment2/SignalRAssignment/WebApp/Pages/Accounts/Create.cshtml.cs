using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly WebApp.Data.AssDBContext _context;

        public CreateModel(WebApp.Data.AssDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Account Account { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            var acc = new Account();
            acc.UserName = Account.UserName;
            acc.Password = Account.Password;
            acc.FullName = Account.FullName;
            acc.Type = Account.Type;
            _context.Accounts.Add(acc);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
