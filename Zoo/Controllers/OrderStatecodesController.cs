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
    public class OrderStatecodesController : Controller
    {
        private readonly zooContext _context;

        public OrderStatecodesController(zooContext context)
        {
            _context = context;
        }

        // GET: OrderStatecodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderStatecodes.ToListAsync());
        }

        // GET: OrderStatecodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatecode = await _context.OrderStatecodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderStatecode == null)
            {
                return NotFound();
            }

            return View(orderStatecode);
        }

        // GET: OrderStatecodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderStatecodes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] OrderStatecode orderStatecode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderStatecode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderStatecode);
        }

        // GET: OrderStatecodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatecode = await _context.OrderStatecodes.FindAsync(id);
            if (orderStatecode == null)
            {
                return NotFound();
            }
            return View(orderStatecode);
        }

        // POST: OrderStatecodes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] OrderStatecode orderStatecode)
        {
            if (id != orderStatecode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderStatecode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderStatecodeExists(orderStatecode.Id))
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
            return View(orderStatecode);
        }

        // GET: OrderStatecodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatecode = await _context.OrderStatecodes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderStatecode == null)
            {
                return NotFound();
            }

            return View(orderStatecode);
        }

        // POST: OrderStatecodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderStatecode = await _context.OrderStatecodes.FindAsync(id);
            _context.OrderStatecodes.Remove(orderStatecode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderStatecodeExists(int id)
        {
            return _context.OrderStatecodes.Any(e => e.Id == id);
        }
    }
}
