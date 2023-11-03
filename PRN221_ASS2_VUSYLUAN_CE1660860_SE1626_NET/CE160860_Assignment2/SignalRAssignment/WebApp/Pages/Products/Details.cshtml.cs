using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly WebApp.Data.AssDBContext _context;

        public DetailsModel(WebApp.Data.AssDBContext context)
        {
            _context = context;
        }

      public Product Product { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                Product = Product;
                ViewData["cat"] = _context.Categories.FirstOrDefault(x => x.CategoryId == Product.CategoryId).CategoryName;
                ViewData["sup"] = _context.Suppliers.FirstOrDefault(x => x.SupplierId == Product.SupplierId).CompanyName;
            }
            return Page();
        }
    }
}
