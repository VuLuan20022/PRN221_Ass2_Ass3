using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly WebApp.Data.AssDBContext _context;

        public List<Category> listCat;
        public List<Supplier> listSup;
        public CreateModel(WebApp.Data.AssDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
        ViewData["SupplierId"] = new SelectList(_context.Suppliers, "SupplierId", "SupplierId");
            listCat = _context.Categories.ToList();
            listSup = _context.Suppliers.ToList();
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Product.ProductImage))
            {
                Product.ProductImage = "https://www.pizzaexpress.vn/wp-content/uploads/2018/08/shutterstock_657998458-780x490.jpg";
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
