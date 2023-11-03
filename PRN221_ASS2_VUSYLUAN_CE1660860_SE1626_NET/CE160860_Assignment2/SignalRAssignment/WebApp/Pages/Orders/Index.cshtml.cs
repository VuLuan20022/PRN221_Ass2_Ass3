using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly WebApp.Data.AssDBContext _context;

        public IndexModel(WebApp.Data.AssDBContext context)
        {
            _context = context;
        }
        public string TopCustomer { get; set; }
        public string TopShippingAddress { get; set; }

       

        public IList<Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Orders != null)
            {
                Order = await _context.Orders
                        .Include(o => o.Customer).ToListAsync();

                // Get the CustomerName of the top customer instead of CustomerId
                TopCustomer = Order.GroupBy(o => o.Customer.CustomerId)
                                   .OrderByDescending(g => g.Count())
                                   .Select(g => g.First().Customer.ContactName)
                                   .FirstOrDefault();

                // Identify the top shipping address
                TopShippingAddress = Order.GroupBy(o => o.ShipAddress)
                                          .OrderByDescending(g => g.Count())
                                          .Select(g => g.Key)
                                          .FirstOrDefault();
            }
        }

    }
}
