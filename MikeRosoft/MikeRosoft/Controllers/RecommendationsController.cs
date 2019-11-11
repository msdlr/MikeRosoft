using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MikeRosoft.Data;
using MikeRosoft.Models;
using MikeRosoft.Models.RecommendationViewModels;

namespace MikeRosoft.Controllers
{
    [Authorize(Roles = "Admin")]
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
                .Include(m => m.ProductRecommendations).ThenInclude(m => m.product)
                .Include(m => m.admin)
                .FirstAsync(m => m.IdRecommendation == id);
                //.FirstOrDefaultAsync(m => m.IdRecommendation == id);
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

        //GET: Recommendations/SelectProductsForRecommendation
        public IActionResult SelectProductsForRecommendation(string productTitle, string productBrandSelected, int productPrice = -1, int productRate = -1)
        {
            SelectProductsForRecommendationViewModel selectProducts = new SelectProductsForRecommendationViewModel();
            //It will be used to fill in a drop-down control
            selectProducts.Brands = new SelectList(_context.Brand.Select(g => g.Name).ToList());
            //It will filter that product that have enough quantity in stock
            selectProducts.Products = _context.Products.Include(m => m.brand).Where(m => m.Stock > 0);
            //For title
            if(productTitle != null)
            {
                selectProducts.Products = selectProducts.Products.Where(m => m.Title.Contains(productTitle));

            }
            //For price
            if (productPrice > 0)
            {
                selectProducts.Products = selectProducts.Products.Where(m => m.Price >= productPrice);

            }
            //For rate
            if (productRate >= 0)
            {
                selectProducts.Products = selectProducts.Products.Where(m => m.Rate >= productRate);

            }

            if (productBrandSelected != null)
            {
                selectProducts.Products = selectProducts.Products.Where(m => m.brand.Name.Contains(productBrandSelected));

            }
            selectProducts.Products.ToList();
            return View(selectProducts);
        }

        //POST: Recommendations/SelectProductForRecommendation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectProductsForRecommendation(SelectedProductsForRecommendationViewModel selectedProducts)
        {
            if (selectedProducts.IdsToAdd != null)
            {
                return RedirectToAction("Create", selectedProducts);
            }
            else
            {
                //a message error will be shown to the admin in case no products were selected
                ModelState.AddModelError(string.Empty, "You must select at least one product");
                //we will recreate the view model again
                SelectProductsForRecommendationViewModel selectProducts = new SelectProductsForRecommendationViewModel();
                selectProducts.Brands = new SelectList(_context.Brand.Select(g => g.Name).ToList());
                selectProducts.Products = _context.Products.Include(m => m.brand).Where(m => m.Stock > 0).ToList();
                return View(selectProducts);
            }
        }
    }
}
