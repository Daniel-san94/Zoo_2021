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
    public class CategoriesController : Controller
    {
        private readonly zooContext _context;

        public CategoriesController(zooContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var zooContext = _context.Categories.Include(c => c.Image);
            return View(await zooContext.ToListAsync());
        }
        public async Task<IActionResult> Shop()
        {

           // var itemList = await _context.Items.Where(x => x.CategoryId == x.Id).ToListAsync();

            var zooContext = _context.Categories.Include(c => c.Image);
            return View(await zooContext.ToListAsync());
            //return View(itemList);
        }

        //ez a metódus rendezi kategóriákba a termékeket és név illetve ár szerinti rendezés van benne
        public async Task<IActionResult> GetCategoryDetails(string sortItem, int Id, string searchString)
        {
            //ez egy linq lekérdezés ahol az item id egyezik a category id-vel és be vannak includeolva az idegen kulcsok
            var itemList = await _context.Items.Where(x => x.CategoryId == Id).Include(i => i.Category).Include(i => i.Image).Include(i => i.Local).ToListAsync();

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortItem) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortItem == "Price" ? "price_desc" : "Price";
            ViewData["CurrentFilter"] = searchString;
            var items = from i in itemList
                        select i;

            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(i => i.Name.Contains(searchString));
                                       
            }

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


            return View(items);

        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Link");
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ImageId")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Link", category.ImageId);
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Link", category.ImageId);
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageId")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Link", category.ImageId);
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .Include(c => c.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
