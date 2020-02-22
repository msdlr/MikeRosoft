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
          return View(_context.Recommendations.Include(r => r.Admin).Where(r => r.Admin.Name.Equals(User.Identity.Name)).OrderByDescending(r => r.Date).ToList());
          //Devuelve una vista con todas las recomendaciones por orden descendente respecto a la fecha 
           // return View(_context.Recommendations.OrderByDescending(p => p.Date).ToList());
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
            if(selectedProducts.IdsToAdd == null)
            {
                ModelState.AddModelError("ProductNoSelected", "You should select at least a Product to be recommended, please");
            }
            else
            {
                //Cuando seleccionamos el producto, buscamos el producto de la lista de productos que tenga el id del seleccionado y lo añadimos a la lista de productos para recomendar
                foreach(string ids in selectedProducts.IdsToAdd)
                {
                    id = int.Parse(ids);
                    product = _context.Products.Include(p => p.brand).FirstOrDefault<Product>(p => p.id.Equals(id));
                    product.ProductRecommendations.Add(new ProductRecommend() { Product = product });
                }
            }
            //Tomamos los datos del administrador que ha iniciado sesión y lo guardamos en los datos del administrador de la recomendación
            Admin admin = _context.Admins.First(u => u.UserName.Equals(User.Identity.Name));
            recommendation.Name = admin.Name;
            recommendation.FirstSurname = admin.FirstSurname;
            recommendation.SecondSurname = admin.SecondSurname;
            recommendation.DNI = admin.DNI;

            return View(recommendation);
        }

        // POST: Recommendations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(RecommendationCreateViewModel recommendationCreateViewModel, IList<ProductRecommend> productRecommends)
        {
            //Creamos un objeto para producto, para administrador y para recomendaciones. Creamos una lista vacía de productos para recomendar
            Product product;
            Admin admin;
            Recommendation recommendation = new Recommendation();
            recommendation.ProductRecommendations = new List<ProductRecommend>();
            ModelState.Clear();
            foreach (ProductRecommend prod in productRecommends)
            {
                product = await _context.Products.FirstOrDefaultAsync<Product>(p => p.id == prod.Product.id);
                if (product.stock == 0)
                {
                    //Si no hay productos en stock, no tiene sentido recomendarlos
                    ModelState.AddModelError("", $"There are no that product in stock");
                    recommendationCreateViewModel.ProductRecommendations = productRecommends;
                }
                else
                {
                    //Si hay productos, igualamos el producto actual de la lista a producto, añadimos la recomendación y el producto a la lista de productos para recomendar
                    //Recordar que prod es un elemento de la clase ProductRecommend, por lo que relaciona un producto y una recomendacion, y luego añade el producto a la lista de productos de la recomendacion
                    prod.Product = product;
                    prod.Recommendation = recommendation;
                    recommendation.ProductRecommendations.Add(prod);
                }
            }
            //Añadimos el administrador actual al objeto del administrador que hemos creado 
            admin = _context.Admins.First(u => u.UserName.Equals(User.Identity.Name));

            if (ModelState.ErrorCount > 0)
            {
                //Si hay ningún error, añade todos los datos del administrador actual a la vista del create
                recommendationCreateViewModel.Name = admin.Name;
                recommendationCreateViewModel.FirstSurname = admin.FirstSurname;
                recommendationCreateViewModel.SecondSurname = admin.SecondSurname;
                recommendationCreateViewModel.DNI = admin.DNI;
                return View(recommendationCreateViewModel);
            }
            //Si no hay ningún error pero no ha seleccionado ningun producto, añade los datos del admistrador a la vista y muestra un mensaje 
            if(recommendation.ProductRecommendations.Count == 0)
            {
                recommendationCreateViewModel.Name = admin.Name;
                recommendationCreateViewModel.FirstSurname = admin.FirstSurname;
                recommendationCreateViewModel.SecondSurname = admin.SecondSurname;
                recommendationCreateViewModel.DNI = admin.DNI;
                ModelState.AddModelError("", $"Please select at least a product to make a recommendation or cancel this action");
                recommendationCreateViewModel.ProductRecommendations = productRecommends;
                return View(recommendationCreateViewModel);
            }
            //Si no hay ningun error y ha seleccionado al menos un producto, crea la recomendacion y pasa a la vista del details 
            recommendation.Admin = admin;
            recommendation.Date = DateTime.Now;
            recommendation.NameRec = recommendationCreateViewModel.NameRec;
            recommendation.Description = recommendationCreateViewModel.Description;
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
