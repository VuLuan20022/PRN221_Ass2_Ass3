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
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IHubContext<SignalrServer> _signalRHub;
        public PostsController(ApplicationDBContext context, IHubContext<SignalrServer> signalRHub)
        {
            _context = context;
            _signalRHub = signalRHub;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var applicationDBContext = _context.Posts.Include(p => p.AppUser).Include(p => p.PostCategory);
            return View(await applicationDBContext.ToListAsync());
        }

        [HttpGet]
        public  IActionResult GetPosts()
        {
            var res = _context.Posts.ToList();
            return Ok(res);
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.AppUser)
                .Include(p => p.PostCategory)
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.AppUsers, "UserID", "FullName");
            ViewData["CategoryID"] = new SelectList(_context.PostCategories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PostID,AuthorID,CreatedDate,UpdatedDate,Title,Content,PublicStatus,CategoryID")] Post post)
        {

                _context.Add(post);
                await _context.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadPosts");
                return RedirectToAction(nameof(Index));
 
            ViewData["AuthorID"] = new SelectList(_context.AppUsers, "UserID", "FullName", post.AuthorID);
            ViewData["CategoryID"] = new SelectList(_context.PostCategories, "CategoryID", "CategoryName", post.CategoryID);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.AppUsers, "UserID", "FullName", post.AuthorID);
            ViewData["CategoryID"] = new SelectList(_context.PostCategories, "CategoryID", "CategoryName", post.CategoryID);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int postID, [Bind("PostID,AuthorID,CreatedDate,UpdatedDate,Title,Content,PublicStatus,CategoryID")] Post post)
        {
            if (postID != post.PostID)
            {
                return NotFound();
            }

         
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                    await _signalRHub.Clients.All.SendAsync("LoadPosts");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.PostID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            ViewData["AuthorID"] = new SelectList(_context.AppUsers, "UserID", "FullName", post.AuthorID);
            ViewData["CategoryID"] = new SelectList(_context.PostCategories, "CategoryID", "CategoryName", post.CategoryID);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Posts == null)
            {
                return NotFound();
            }

            var post = await _context.Posts
                .Include(p => p.AppUser)
                .Include(p => p.PostCategory)
                .FirstOrDefaultAsync(m => m.PostID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int postID)
        {
            if (_context.Posts == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Posts'  is null.");
            }
            var post = await _context.Posts.FindAsync(postID);
            if (post != null)
            {
                _context.Posts.Remove(post);
            }
            
            await _context.SaveChangesAsync();
            await _signalRHub.Clients.All.SendAsync("LoadPosts");
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
          return (_context.Posts?.Any(e => e.PostID == id)).GetValueOrDefault();
        }
    }
}
