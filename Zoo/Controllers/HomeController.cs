using Zoo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zoo.Controllers
{
    public class HomeController : Controller
    {
        private readonly zooContext _context;

        public HomeController(zooContext context)
        {
            _context = context;
        }
        public IActionResult Intro()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Zoo1()
        {
            return View();
        }

        public IActionResult Zoo2()
        {
            return View();
        }

        public IActionResult Zoo3()
        {
            return View();
        }

        public IActionResult Zoo4()
        {
            return View();
        }

        public IActionResult Shop()
        {
            var items = _context.Items.ToList();

            return View();
        }
    }
}

