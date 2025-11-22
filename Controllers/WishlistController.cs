using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using VmoragaCollectionManager.Data;
using VmoragaCollectionManager.Models;
using System.Threading.Tasks;
using System.Linq;

namespace VmoragaCollectionManager.Controllers
{
    [Authorize]
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public WishlistController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Add(int collectionItemId, bool owned)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null) return Unauthorized();

            var exists = _context.WishlistItems.Any(w => w.UserId == userId && w.CollectionItemId == collectionItemId);
            if (!exists)
            {
                var wishlistItem = new WishlistItem
                {
                    UserId = userId,
                    CollectionItemId = collectionItemId,
                    Owned = owned
                };
                _context.WishlistItems.Add(wishlistItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Details", "Collections", new { id = _context.CollectionItems.Find(collectionItemId)?.CollectionId });
        }
    }
}
