using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zoo.Helpers;
using Zoo.Models;


namespace Zoo.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly zooContext _context;

        public CheckoutController(zooContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");
            var osszeg = 0;

            foreach (var item in cart)
            {
                var finditem = await _context.Items.Where(x => x.Id == item.ItemId).Include(i => i.Image).FirstOrDefaultAsync();
                item.Item = finditem;
                osszeg += item.Quantity * item.Item.Price;
            }
            ViewBag.cart = cart;
            ViewBag.total = osszeg;
            return View();
        }
    }
}
