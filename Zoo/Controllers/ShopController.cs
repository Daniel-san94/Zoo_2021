using Zoo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Zoo.Controllers
{
    public class ShopController : Controller
    {
        private readonly zooContext _context;

        public ShopController(zooContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Invoice()
        {
            return View();
        }

        public IActionResult ecommerce()
        {
            return View();
        }
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
        public async Task<IActionResult> Shop()
        {

            // var itemList = await _context.Items.Where(x => x.CategoryId == x.Id).ToListAsync();

            var zooContext = _context.Categories.Include(c => c.Image);
            return View(await zooContext.ToListAsync());
            //return View(itemList);
        }
        //biztosítja az itemek rendezhetőségét, amikor az összes terméket nézzük a weboldalon
        //akkor ez biztositja a rendezhetőséget !!!EZT JAVÍTANI KELL MAJD!!!
        public async Task<IActionResult> AllItem(string sortItem, int Id, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortItem) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortItem == "Price" ? "price_desc" : "Price";
            ViewData["CurrentFilter"] = searchString;


            var items = from i in _context.Items.Include(i => i.Category).Include(i => i.Image).Include(i => i.Local)
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
            //ezeket ki kellett kommentálni, mert beletettem a az items változóba a zooContext értékét
            //var zooContext = _context.Items.Include(i => i.Category).Include(i => i.Image).Include(i => i.Local);
            //return View(await zooContext.ToListAsync());
            return View(await items.AsNoTracking().ToListAsync());

        }
        //public async Task<IActionResult> AllItem()
        //{
        //    var zooContext = _context.Items.Include(i => i.Category).Include(i => i.Image).Include(i => i.Local);
        //    return View(await zooContext.ToListAsync());
        //}

    }
}

