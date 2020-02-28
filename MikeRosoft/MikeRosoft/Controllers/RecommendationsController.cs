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
        { //Devuelve una vista que contiene todas las recomendaciones de el administrador que ha iniciado sesión en orden descendente
          //return View(_context.Recommendations.Include(r => r.Admin).Where(r => r.Admin.Name.Equals(User.Identity.Name)).OrderByDescending(r => r.Date).ToList());
          //Devuelve una vista con todas las recomendaciones por orden descendente respecto a la fecha 
          // return View(_context.Recommendations.OrderByDescending(p => p.Date).ToList());

            IEnumerable<Recommendation> recom = _context.Recommendations.Include(r => r.ProductRecommendations).ThenInclude(r => r.Product).Include(r => r.Admin);
            return View(recom);
        }

        // GET: Recommendations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            //Si no encuentra una recomendacion con ese id para mostrar sus detalles, devuelve un Not Found
            if (id == null)
            {
                return NotFound();
            }

            /*var recommendation = await _context.Recommendations
                .Include(m => m.ProductRecommendations).ThenInclude(m => m.product)
                .Include(m => m.admin)
                .FirstAsync(m => m.IdRecommendation == id);*/
            //.FirstOrDefaultAsync(m => m.IdRecommendation == id);


            var recommendation = _context.Recommendations.Include(r => r.ProductRecommendations).ThenInclude<Recommendation, ProductRecommend, Product>(p => p.Product).Include(p => p.Admin).Where(p => p.IdRecommendation == id).ToList();

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
            ProductRecommend productRecommend;

            //Cuando seleccionamos el producto, buscamos el producto de la lista de productos que tenga el id del seleccionado y lo añadimos a la lista de productos para recomendar
           /* foreach (string ids in selectedProducts.IdsToAdd)
            {
                id = int.Parse(ids);
                //product = _context.Products.Include(p => p.brand).FirstOrDefault<Product>(p => p.id.Equals(id));
                product = _context.Products.Find(id);
                productRecommend.Product = product;
                recommendation.ProductRecommendations.Add(productRecommend);
            }*/
            for(int i = 0; i < selectedProducts.IdsToAdd.Length; i++)
            {
                id = int.Parse(selectedProducts.IdsToAdd[i]);
                product = _context.Products.Find(id);
                productRecommend = new ProductRecommend() { Product = product };
                recommendation.ProductRecommendations.Add(productRecommend);
            }
        
            //Tomamos los datos del administrador que ha iniciado sesión y lo guardamos en los datos del administrador de la recomendación
            Admin admin = _context.Admins.First(u => u.UserName.Equals(User.Identity.Name));
            recommendation.Name = admin.Name;
            recommendation.FirstSurname = admin.FirstSurname;
            recommendation.SecondSurname = admin.SecondSurname;
            recommendation.DNI = admin.DNI;

            recommendation.Date = DateTime.Now;

            return View(recommendation);
        }

        // POST: Recommendations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(RecommendationCreateViewModel recommendationCreateViewModel, int[] prod)
        {
            Recommendation recom = new Recommendation() { Admin = await _context.Admins.FirstOrDefaultAsync<Admin>(c => c.UserName.Equals(User.Identity.Name)),
             Date= DateTime.Now, Description = recommendationCreateViewModel.Description, NameRec = recommendationCreateViewModel.NameRec };
            IList<ProductRecommend> prodrecom = new List<ProductRecommend>();
            foreach(int id in prod)
            {
                prodrecom.Add(new ProductRecommend() { Product = _context.Products.Find(id), ProductId = id, RecommendationId = recom.IdRecommendation});

            }
            
            recom.ProductRecommendations = prodrecom;
            recom.AdminId = recom.Admin.Id.ToString();
            _context.Recommendations.Add(recom);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = recom.IdRecommendation });
        }

        // GET: Recommendations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var recommendation = await _context.Recommendations.FindAsync(id);
            var recommendation = await _context.Recommendations.Include(pr => pr.ProductRecommendations).ThenInclude<Recommendation, ProductRecommend, Product>(p => p.Product).Include(pr => pr.Admin).SingleOrDefaultAsync(r => r.IdRecommendation == id);
            if (recommendation == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Set<Admin>(), "Id", "Id", recommendation.Admin);
            return View(recommendation);
        }

        // POST: Recommendations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRecommendation,AdminId,Date")] Recommendation recommendation)
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

            /*var recommendation = await _context.Recommendations
                .SingleOrDefaultAsync(m => m.IdRecommendation == id);*/
            var recommendation = await _context.Recommendations.SingleOrDefaultAsync(r => r.IdRecommendation == id);
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
            //var recommendation = await _context.Recommendations.FindAsync(id);
            var recommendation = await _context.Recommendations.Include(pr => pr.ProductRecommendations).SingleOrDefaultAsync(pr => pr.IdRecommendation == id);
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
