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
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Wishlist()
        {
            var userId = _userManager.GetUserId(User);
            var wishlist = await _context.WishlistItems
                .Include(w => w.CollectionItem)
                .ThenInclude(ci => ci.Collection)
                .Where(w => w.UserId == userId)
                .ToListAsync();
            return View("~/Views/Wishlist/Index.cshtml", wishlist);
        }
    }
}
