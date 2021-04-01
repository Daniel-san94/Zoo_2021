using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zoo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Zoo.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private zooContext _context;
        public CategoryViewComponent(zooContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var item = await _context.Categories.ToListAsync();
            return View(item);
        }
    }
}
