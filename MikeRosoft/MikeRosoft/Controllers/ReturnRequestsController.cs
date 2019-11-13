using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MikeRosoft.Data;
using MikeRosoft.Models;

namespace MikeRosoft.Controllers
{
    public class ReturnRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReturnRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReturnRequests
        public async Task<IActionResult> Index(string SearchString)
        {
            if (!String.IsNullOrEmpty(SearchString)) {
                var returnRequests = _context.ReturnRequests.Where(s => s.title.Contains(SearchString));
                return View(await returnRequests.ToListAsync());
            } else
                return View(await _context.ReturnRequests.ToListAsync());
        }

        // GET: ReturnRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var returnRequest = await _context.ReturnRequests
                .FirstOrDefaultAsync(m => m.ID == id);
            if (returnRequest == null)
            {
                return NotFound();
            }

            return View(returnRequest);
        }

        // GET: ReturnRequests/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReturnRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,title")] ReturnRequest returnRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(returnRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(returnRequest);
        }

        // GET: ReturnRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var returnRequest = await _context.ReturnRequests.FindAsync(id);
            if (returnRequest == null)
            {
                return NotFound();
            }
            return View(returnRequest);
        }

        // POST: ReturnRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,title")] ReturnRequest returnRequest)
        {
            if (id != returnRequest.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(returnRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReturnRequestExists(returnRequest.ID))
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
            return View(returnRequest);
        }

        // GET: ReturnRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var returnRequest = await _context.ReturnRequests
                .FirstOrDefaultAsync(m => m.ID == id);
            if (returnRequest == null)
            {
                return NotFound();
            }

            return View(returnRequest);
        }

        // POST: ReturnRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var returnRequest = await _context.ReturnRequests.FindAsync(id);
            _context.ReturnRequests.Remove(returnRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReturnRequestExists(int id)
        {
            return _context.ReturnRequests.Any(e => e.ID == id);
        }
    }
}
