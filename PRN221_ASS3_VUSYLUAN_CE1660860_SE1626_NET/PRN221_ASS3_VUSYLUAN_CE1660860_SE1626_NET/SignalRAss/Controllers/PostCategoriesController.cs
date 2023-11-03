using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRAss.Models;

namespace SignalRAss.Controllers
{

    [Authorize(Policy = "CanAccessController")]
    public class PostCategoriesController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IHubContext<SignalrServer> _signalRHub;
        public PostCategoriesController(ApplicationDBContext context, IHubContext<SignalrServer> signalRHub)
        {
            _context = context;
            _signalRHub = signalRHub;
        }

        // GET: PostCategories
        public async Task<IActionResult> Index()
        {
              return _context.PostCategories != null ? 
                          View(await _context.PostCategories.ToListAsync()) :
                          Problem("Entity set 'ApplicationDBContext.PostCategories'  is null.");
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var res = await _context.PostCategories.ToListAsync();
            return Ok(res);
        }

        // GET: PostCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PostCategories == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategories
                .FirstOrDefaultAsync(m => m.CategoryID == id);
            if (postCategory == null)
            {
                return NotFound();
            }

            return View(postCategory);
        }

        // GET: PostCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryID,CategoryName,Description")] PostCategory postCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postCategory);
                await _context.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadCategorys");
                return RedirectToAction(nameof(Index));
            }
            return View(postCategory);
        }

        // GET: PostCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PostCategories == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategories.FindAsync(id);
            if (postCategory == null)
            {
                return NotFound();
            }
            return View(postCategory);
        }

        // POST: PostCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int categoryID, [Bind("CategoryID,CategoryName,Description")] PostCategory postCategory)
        {
            if (categoryID != postCategory.CategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postCategory);
                    await _context.SaveChangesAsync();
                    await _signalRHub.Clients.All.SendAsync("LoadCategorys");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostCategoryExists(postCategory.CategoryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(postCategory);
        }

        // GET: PostCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PostCategories == null)
            {
                return NotFound();
            }

            var postCategory = await _context.PostCategories
                .FirstOrDefaultAsync(m => m.CategoryID == id);
            if (postCategory == null)
            {
                return NotFound();
            }

            return View(postCategory);
        }

        // POST: PostCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int categoryID)
        {
            if (_context.PostCategories == null)
            {
                return Problem("Entity set 'ApplicationDBContext.PostCategories'  is null.");
            }
            var postCategory = await _context.PostCategories.FindAsync(categoryID);
            if (postCategory != null)
            {
                _context.PostCategories.Remove(postCategory);
            }
            
            await _context.SaveChangesAsync();
            await _signalRHub.Clients.All.SendAsync("LoadCategorys");
            return RedirectToAction(nameof(Index));
        }

        private bool PostCategoryExists(int id)
        {
          return (_context.PostCategories?.Any(e => e.CategoryID == id)).GetValueOrDefault();
        }
    }
}
