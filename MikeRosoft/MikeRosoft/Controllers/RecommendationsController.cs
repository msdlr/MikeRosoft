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
    public class RecommendationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RecommendationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Recommendations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Recommendations.ToListAsync());
        }

        // GET: Recommendations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recommendation = await _context.Recommendations
                .FirstOrDefaultAsync(m => m.IdRecommendation == id);
            if (recommendation == null)
            {
                return NotFound();
            }

            return View(recommendation);
        }

        // GET: Recommendations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recommendations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRecommendation,name,date,description")] Recommendation recommendation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recommendation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(recommendation);
        }

        // GET: Recommendations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recommendation = await _context.Recommendations.FindAsync(id);
            if (recommendation == null)
            {
                return NotFound();
            }
            return View(recommendation);
        }

        // POST: Recommendations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRecommendation,name,date,description")] Recommendation recommendation)
        {
            if (id != recommendation.IdRecommendation)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recommendation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecommendationExists(recommendation.IdRecommendation))
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
            return View(recommendation);
        }

        // GET: Recommendations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recommendation = await _context.Recommendations
                .FirstOrDefaultAsync(m => m.IdRecommendation == id);
            if (recommendation == null)
            {
                return NotFound();
            }

            return View(recommendation);
        }

        // POST: Recommendations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recommendation = await _context.Recommendations.FindAsync(id);
            _context.Recommendations.Remove(recommendation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecommendationExists(int id)
        {
            return _context.Recommendations.Any(e => e.IdRecommendation == id);
        }
    }
}
