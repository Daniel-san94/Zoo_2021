using Zoo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zoo.Controllers
{
    public class ShopController : Controller
    {
        private readonly zooContext _context;

        public ShopController(zooContext context)
        {
            _context = context;
        }
        public IActionResult Invoice()
        {
            return View();
        }

        public IActionResult ecommerce()
        {
            return View();
        }

    }
}

