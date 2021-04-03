using Zoo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IActionResult> Zoo1()
        {

            IQueryable<Image> kepek = from m in _context.Images
                                      select m;
            kepek = kepek.Where(s => s.Local_id == 1);
            return View(await kepek.ToListAsync());
            //return View();
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
            return View();
        }
        public IActionResult Admin()
        {
            return View();
        }

    }
}

