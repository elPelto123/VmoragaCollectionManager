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
    public class UserCollectionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserCollectionsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Vista de colecciones seleccionadas por el usuario
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var selectedCollections = await _context.UserSelectedCollections
                .Include(uc => uc.Collection)
                .Where(uc => uc.UserId == userId)
                .ToListAsync();
            return View(selectedCollections);
        }

        // POST: UserCollections/Add
        [HttpPost]
        public async Task<IActionResult> Add(int collectionId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            // Verificar si ya está seleccionada
            var exists = await _context.UserSelectedCollections.AnyAsync(uc => uc.UserId == userId && uc.CollectionId == collectionId);
            if (!exists)
            {
                var userCollection = new UserSelectedCollection
                {
                    UserId = userId,
                    CollectionId = collectionId
                };
                _context.UserSelectedCollections.Add(userCollection);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}
