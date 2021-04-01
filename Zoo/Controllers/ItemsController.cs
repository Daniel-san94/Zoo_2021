using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zoo.Models;

namespace Zoo.Controllers
{
    public class ItemsController : Controller
    {
        private readonly zooContext _context;

        public ItemsController(zooContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index()
        {
            var zooContext = _context.Items.Include(i => i.Category).Include(i => i.Image).Include(i => i.Local);
            return View(await zooContext.ToListAsync());
        }

        //biztosítja az itemek rendezhetőségét, amikor az összes terméket nézzük a weboldalon
        //akkor ez biztositja a rendezhetőséget !!!EZT JAVÍTANI KELL MAJD!!!
        public async Task<IActionResult> Shop(string sortItem, int Id)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortItem) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortItem == "Price" ? "price_desc" : "Price";
            var items = from i in _context.Items.Include(i => i.Category).Include(i => i.Image).Include(i => i.Local)
            select i;
            switch (sortItem)
            {
                case "name_desc":
                    items = items.OrderByDescending(i => i.Name);
                    break;
                case "Price":
                    items = items.OrderBy(i => i.Price);
                    break;
                case "price_desc":
                    items = items.OrderByDescending(i => i.Price);
                    break;
                default:
                    items = items.OrderBy(i => i.Name);
                    break;
            }
            //ezeket ki kellett kommentálni, mert beletettem a az items változóba a zooContext értékét
            //var zooContext = _context.Items.Include(i => i.Category).Include(i => i.Image).Include(i => i.Local);
            //return View(await zooContext.ToListAsync());
            return View(await items.AsNoTracking().ToListAsync());

        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .Include(i => i.Image)
                .Include(i => i.Local)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Link");
            ViewData["LocalId"] = new SelectList(_context.Locals, "Id", "Address");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ImageId,LocalId,Name,Description,Price,CategoryId")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", item.CategoryId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Link", item.ImageId);
            ViewData["LocalId"] = new SelectList(_context.Locals, "Id", "Address", item.LocalId);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", item.CategoryId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Link", item.ImageId);
            ViewData["LocalId"] = new SelectList(_context.Locals, "Id", "Address", item.LocalId);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ImageId,LocalId,Name,Description,Price,CategoryId")] Item item)
        {
            if (id != item.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", item.CategoryId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Link", item.ImageId);
            ViewData["LocalId"] = new SelectList(_context.Locals, "Id", "Address", item.LocalId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Category)
                .Include(i => i.Image)
                .Include(i => i.Local)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
