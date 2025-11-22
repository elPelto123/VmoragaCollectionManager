using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VmoragaCollectionManager.Data;
using VmoragaCollectionManager.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VmoragaCollectionManager.Controllers
{
    [Authorize]
    public class CollectionItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CollectionItemsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: CollectionItems/Create?collectionId=1
        public IActionResult Create(int collectionId)
        {
            ViewBag.CollectionId = collectionId;
            return View();
        }

        // POST: CollectionItems/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int collectionId, [Bind("Name,Description")] CollectionItem item, IFormFile imageFile)
        {
            //if (ModelState.IsValid)
            //{
                item.CollectionId = collectionId;
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploads = Path.Combine(_environment.WebRootPath, "images");
                    Directory.CreateDirectory(uploads);
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(uploads, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    item.ImagePath = "/images/" + fileName;
                }
                _context.CollectionItems.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Collections", new { id = collectionId });
            //}
            ViewBag.CollectionId = collectionId;
            return View(item);
        }

        // GET: CollectionItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.CollectionItems.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: CollectionItems/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,CollectionId")] CollectionItem item, IFormFile imageFile)
        {
            if (id != item.Id) return NotFound();
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var uploads = Path.Combine(_environment.WebRootPath, "images");
                    Directory.CreateDirectory(uploads);
                    var fileName = Path.GetFileName(imageFile.FileName);
                    var filePath = Path.Combine(uploads, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }
                    item.ImagePath = "/images/" + fileName;
                }
                _context.Update(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Collections", new { id = item.CollectionId });
            }
            return View(item);
        }

        // GET: CollectionItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var item = await _context.CollectionItems.FindAsync(id);
            if (item == null) return NotFound();
            return View(item);
        }

        // POST: CollectionItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.CollectionItems.FindAsync(id);
            if (item == null) return NotFound();
            int collectionId = item.CollectionId;
            _context.CollectionItems.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Collections", new { id = collectionId });
        }
    }
}
