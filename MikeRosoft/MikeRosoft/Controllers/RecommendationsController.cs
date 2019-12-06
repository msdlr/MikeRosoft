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
            return View(_context.Recommendations.Include(p => p.admin).Where(p => p.admin.Name.Equals(User.Identity.Name)).OrderByDescending(p => p.date).ToList());
        }

        // GET: Recommendations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var recommendation = await _context.Recommendations
                .Include(m => m.ProductRecommendations).ThenInclude(m => m.product)
                .Include(m => m.admin)
                .FirstAsync(m => m.IdRecommendation == id);*/
            //.FirstOrDefaultAsync(m => m.IdRecommendation == id);
            var recommendation = _context.Recommendations.Include(p => p.ProductRecommendations).ThenInclude<Recommendation, ProductRecommend, Product>(p => p.product).Include(p => p.admin).Where(p => p.IdRecommendation == id).ToList();

            if (recommendation.Count == 0)
            {
                return NotFound();
            }

            return View(recommendation.First());
        }

        // GET: Recommendations/Create
        public IActionResult Create(SelectedProductsForRecommendationViewModel selectedProducts)
        {
            Product product;
            int id;

            RecommendationCreateViewModel recommendation = new RecommendationCreateViewModel();
            recommendation.ProductRecommendations = new List<ProductRecommend>();
            if(selectedProducts.IdsToAdd == null)
            {
                ModelState.AddModelError("ProductNoSelected", "You should select at least a Product to be recommended, please");
            }
            else
            {
                foreach(string ids in selectedProducts.IdsToAdd)
                {
                    id = int.Parse(ids);
                    product = _context.Products.Include(m => m.brand).FirstOrDefault<Product>(m => m.id.Equals(id));
                    product.ProductRecommendations.Add(new ProductRecommend() { product = product });
                }
            }
            Admin admin = _context.Admins.First(u => u.UserName.Equals(User.Identity.Name));
            recommendation.Name = admin.Name;
            recommendation.FirstSurname = admin.FirstSurname;
            recommendation.SecondSurname = admin.SecondSurname;

            return View(recommendation);
        }

        // POST: Recommendations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(RecommendationCreateViewModel recommendationCreateViewModel, IList<ProductRecommend> productRecommends)
        {
            Product product;
            Admin admin;
            Recommendation recommendation = new Recommendation();
            recommendation.ProductRecommendations = new List<ProductRecommend>();
            ModelState.Clear();
            foreach (ProductRecommend prod in productRecommends)
            {
                product = await _context.Products.FirstOrDefaultAsync<Product>(m => m.id == prod.product.id);
                if (product.stock == 0)
                {
                    ModelState.AddModelError("", $"There are no that product in stock");
                    recommendationCreateViewModel.ProductRecommendations = productRecommends;
                }
                else
                {
                    prod.product = product;
                    prod.recommendation = recommendation;
                    recommendation.ProductRecommendations.Add(prod);
                }
            }
            admin = _context.Admins.First(u => u.UserName.Equals(User.Identity.Name));

            if (ModelState.ErrorCount > 0)
            {
                recommendationCreateViewModel.Name = admin.Name;
                recommendationCreateViewModel.FirstSurname = admin.FirstSurname;
                recommendationCreateViewModel.SecondSurname = admin.SecondSurname;
                return View(recommendationCreateViewModel);
            }
            if(recommendation.ProductRecommendations.Count == 0)
            {
                recommendationCreateViewModel.Name = admin.Name;
                recommendationCreateViewModel.FirstSurname = admin.FirstSurname;
                recommendationCreateViewModel.SecondSurname = admin.SecondSurname;
                ModelState.AddModelError("", $"Please select at least a product to make a recommendation or cancel this action");
                recommendationCreateViewModel.ProductRecommendations = productRecommends;
                return View(recommendationCreateViewModel);
            }

            recommendation.admin = admin;
            recommendation.date = DateTime.Now;
            recommendation.name = recommendationCreateViewModel.name;
            recommendation.description = recommendationCreateViewModel.description;
            _context.Add(recommendation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = recommendation.IdRecommendation });
            
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
            ViewData["AdminId"] = new SelectList(_context.Set<Admin>(), "Id", "Id", recommendation.AdminId);
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
            ViewData["AdminId"] = new SelectList(_context.Set<Admin>(), "Id", "Id", recommendation.AdminId);
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
                .SingleOrDefaultAsync(m => m.IdRecommendation == id);
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
        public IActionResult SelectProductsForRecommendation(string productTitle, string productBrandSelected, float productPrice = -1, int productRate = -1)
        {
            SelectProductsForRecommendationViewModel selectProducts = new SelectProductsForRecommendationViewModel();
            //It will be used to fill in a drop-down control
            selectProducts.Brands = new SelectList(_context.Brand.Select(g => g.Name).ToList());
            //It will filter that product that have enough quantity in stock
            selectProducts.Products = _context.Products.Include(m => m.brand).Where(m => m.stock > 0);
            //For title
            if(productTitle != null)
            {
                selectProducts.Products = selectProducts.Products.Where(m => m.title.Contains(productTitle));

            }
            //For price
            if (productPrice > 0)
            {
                selectProducts.Products = selectProducts.Products.Where(m => m.precio >= productPrice);

            }
            //For rate
            if (productRate >= 0)
            {
                selectProducts.Products = selectProducts.Products.Where(m => m.rate >= productRate);

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
                selectProducts.Products = _context.Products.Include(m => m.brand).Where(m => m.stock > 0).ToList();
                return View(selectProducts);
            }
        }
    }
}
