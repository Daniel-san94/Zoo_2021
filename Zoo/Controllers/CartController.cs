using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Zoo.Areas.Identity.Data;
using Zoo.Helpers;
using Zoo.Models;



namespace Zoo.Controllers
{
    public class CartController : Controller
    {

        private readonly zooContext _context;

        public CartController(zooContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
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
        private int isExist(int id)
        {
            List<OrderItem> cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].ItemId.Equals(id))
                {
                    return i;
                }

            }
            return -1;
        }
      
        public IActionResult Buy(int id)
        {

            if (SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart") == null)
            {
                List<OrderItem> cart = new List<OrderItem>();

                cart.Add(new OrderItem { ItemId = id, Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                List<OrderItem> cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new OrderItem { ItemId = id, Quantity = 1 });
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);


            }
            return RedirectToAction("Index");
        }
        public IActionResult Remove(int id)
        {
            List<OrderItem> cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");
            int index = isExist(id);
            cart.RemoveAt(index);
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");

        }

        public IActionResult Create()
        {
            Claim userIdClaim;
            String userIdValue = "";
            var claimsIdentity = User.Identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                // the principal identity is a claims identity.
                // now we need to find the NameIdentifier claim
                userIdClaim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    userIdValue = userIdClaim.Value;
                } 
            }
            var cart = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");
            var orderHead = new Order {   
                OrderItems = cart, 
                OrderStatecodeId = 1,
                UserId = userIdValue, //"6a4c72ef-e663-4ad9-af76-d350aa919433",             
            };
            _context.Add(orderHead);
            _context.SaveChanges();
            cart.Clear();
            SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index");

        }
    }
}