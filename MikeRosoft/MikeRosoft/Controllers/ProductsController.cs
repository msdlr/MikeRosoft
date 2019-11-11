using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MikeRosoft.Data;
using MikeRosoft.Models;
using MikeRosoft.Models.OrderViewModels;

namespace MikeRosoft.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        // INDEX ES LO QUE SE CARGA DE PRIMERAS CUANDO ACCEDES A PRODUCTS (al mismo abrir la página)
        public async Task<IActionResult> Index()
        {
            return View(_context.Products.OrderByDescending(p => p.precio).ToList());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,title,description,brand,precio,stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,title,description,brand,precio,stock")] Product product)
        {
            if (id != product.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: SELECT

        //titleSelected y brandSelected se llaman asi porque tienen que ser como el de viewModel
        public IActionResult SelectProductsForBuy(string titleSelected, string brandSelected)
        {
            
            SelectProductsForBuyViewModel selectProducts = new SelectProductsForBuyViewModel();
            //Añade a products todos los productos que están en la base de datos cuyo stock es mayor que 0 
            selectProducts.Products = _context.Products.Where(p => p.stock > 0);

            //Para filtrar por nombre
            if (titleSelected != null)
                selectProducts.Products = selectProducts.Products.Where(p => p.title.Contains(titleSelected));

            //Para filtrar por brand
            if (brandSelected != null)
                selectProducts.Products = selectProducts.Products.Where(p => p.brand.Contains(brandSelected));

            selectProducts.Products.ToList();

            return View(selectProducts);
        }


        // POST: SELECT

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectProductsForBuy(SelectedProductsForBuyViewModel selectedProducts)
        {
            if (selectedProducts.IdsToAdd != null)
                return RedirectToAction("Create", selectedProducts);

            ModelState.AddModelError(string.Empty, "You must select at least one product");
            SelectProductsForBuyViewModel selectProducts = new SelectProductsForBuyViewModel();
            selectProducts.Products = _context.Products.Where(p => p.stock > 0);

            return View(selectProducts);
        }
        


        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.id == id);
        }

    }
}
