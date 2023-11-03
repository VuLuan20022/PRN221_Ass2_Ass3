using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly AssDBContext _context;
        public List<Customer> listCus;
        public CreateModel(AssDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            listCus = _context.Customers.ToList();
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            listCus = _context.Customers.ToList();
            var cus = listCus.FirstOrDefault(x => x.CustomerId == Order.CustomerId);
            Order.Customer = cus;
            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
