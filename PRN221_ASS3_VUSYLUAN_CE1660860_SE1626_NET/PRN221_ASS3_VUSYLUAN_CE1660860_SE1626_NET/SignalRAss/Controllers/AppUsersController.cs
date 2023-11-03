using System;
using System.Collections.Generic;
using System.Data;
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
    public class AppUsersController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IHubContext<SignalrServer> _signalRHub;
        public AppUsersController(ApplicationDBContext context, IHubContext<SignalrServer> signalRHub)
        {
            _context = context;
            _signalRHub = signalRHub;
        }

        // GET: AppUsers
        public async Task<IActionResult> Index()
        {
              return _context.AppUsers != null ? 
                          View(await _context.AppUsers.ToListAsync()) :
                          Problem("Entity set 'ApplicationDBContext.AppUsers'  is null.");
        }
        [HttpGet]
        public async Task<IActionResult> GetAppUsers()
        {
            var res = await _context.AppUsers.ToListAsync();
            return Ok(res);
        }

        // GET: AppUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AppUsers == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUsers
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // GET: AppUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserID,FullName,Address,Email,Password")] AppUser appUser)
        {
            
                _context.Add(appUser);
                await _context.SaveChangesAsync();
                await _signalRHub.Clients.All.SendAsync("LoadAppUsers");
                return RedirectToAction(nameof(Index));
            
            return View(appUser);
        }

        // GET: AppUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AppUsers == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUsers.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            return View(appUser);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int userID, [Bind("UserID,FullName,Address,Email,Password")] AppUser appUser)
        {
            if (userID != appUser.UserID)
            {
                return NotFound();
            }

           
            
                try
                {
                    _context.Update(appUser);
                    await _context.SaveChangesAsync();
                    await _signalRHub.Clients.All.SendAsync("LoadAppUsers");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserExists(appUser.UserID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            
            return View(appUser);
        }

        // GET: AppUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AppUsers == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUsers
                .FirstOrDefaultAsync(m => m.UserID == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int userID)
        {
            if (_context.AppUsers == null)
            {
                return Problem("Entity set 'ApplicationDBContext.AppUsers'  is null.");
            }
            var appUser = await _context.AppUsers.FindAsync(userID);
            if (appUser != null)
            {
                _context.AppUsers.Remove(appUser);
            }
            
            await _context.SaveChangesAsync();
            await _signalRHub.Clients.All.SendAsync("LoadAppUsers");

            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(int id)
        {
          return (_context.AppUsers?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
    }
}
