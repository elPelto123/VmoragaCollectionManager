using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VmoragaCollectionManager.Data;
using VmoragaCollectionManager.Models;
using System.Linq;
using System.Threading.Tasks;

namespace VmoragaCollectionManager.Controllers
{
    [Authorize]
    public class CollectionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CollectionsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Collections
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var ownCollections = await _context.Collections
                .Where(c => c.UserId == userId)
                .ToListAsync();

            var sharedCollections = await _context.Collections
                .Where(c => c.SharedWithUserIds.Contains(userId))
                .ToListAsync();

            ViewBag.SharedCollections = sharedCollections;
            return View(ownCollections);
        }

        // GET: Collections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var collection = await _context.Collections
                .Include(c => c.Items)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (collection == null || collection.UserId != _userManager.GetUserId(User)) return NotFound();
            return View(collection);
        }

        // GET: Collections/Create
        public IActionResult Create() => View();

        // POST: Collections/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description")] Collection collection)
        {
            // Procesar usuarios compartidos
            var sharedUsersRaw = Request.Form["SharedUsers"].ToString();
            if (!string.IsNullOrWhiteSpace(sharedUsersRaw))
            {
                collection.SharedWithUserIds = sharedUsersRaw.Split(',').Select(u => u.Trim()).Where(u => !string.IsNullOrEmpty(u)).ToList();
            }
            else
            {
                collection.SharedWithUserIds = new System.Collections.Generic.List<string>();
            }

            collection.UserId = _userManager.GetUserId(User);
            _context.Add(collection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Collections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var collection = await _context.Collections.FindAsync(id);
            if (collection == null || collection.UserId != _userManager.GetUserId(User)) return NotFound();
            return View(collection);
        }

        // POST: Collections/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Collection collection)
        {
            if (id != collection.Id) return NotFound();
            var userId = _userManager.GetUserId(User);
            if (collection.UserId != userId) return Unauthorized();

            // Procesar usuarios compartidos
            var sharedUsersRaw = Request.Form["SharedUsers"].ToString();
            if (!string.IsNullOrWhiteSpace(sharedUsersRaw))
            {
                collection.SharedWithUserIds = sharedUsersRaw.Split(',').Select(u => u.Trim()).Where(u => !string.IsNullOrEmpty(u)).ToList();
            }
            else
            {
                collection.SharedWithUserIds = new System.Collections.Generic.List<string>();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(collection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Collections.Any(e => e.Id == collection.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(collection);
        }

        // GET: Collections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var collection = await _context.Collections.FindAsync(id);
            if (collection == null || collection.UserId != _userManager.GetUserId(User)) return NotFound();
            return View(collection);
        }

        // POST: Collections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var collection = await _context.Collections.FindAsync(id);
            if (collection == null || collection.UserId != _userManager.GetUserId(User)) return NotFound();
            _context.Collections.Remove(collection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
