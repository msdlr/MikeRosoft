using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MikeRosoft.Data;
using MikeRosoft.Models.ProductViewModels;
using MikeRosoft.Models;

namespace MikeRosoft.Controllers
{
    //[Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string SearchString)
        {
            if (!String.IsNullOrEmpty(SearchString))
            {
                var products = _context.Products.Include(m => m.brand).Where(s => s.title.Contains(SearchString)).OrderBy(m => m.title);
                return View(await products.ToListAsync());
            }
            else
            {
                return View(await _context.Products.Include(m => m.brand).OrderBy(m => m.title).ToListAsync());
            }
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(m => m.brand).SingleOrDefaultAsync(m => m.id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.Brands = new SelectList(_context.Brand.Select(c => c.Name).Distinct());
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            Product product;
            if (ModelState.IsValid)
            {
                product = new Product() { title = model.title, description = model.description, precio = model.precio, stock = model.stock, rate = model.rate };
                Brand brand = await _context.Brand.FirstAsync<Brand>(g => g.Name == model.BrandName);
                product.brand = brand;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Brands = new SelectList(_context.Brand.Select(c => c.Name).Distinct());
            return View(model);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(m => m.brand).SingleOrDefaultAsync(m => m.id == id);
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
        public async Task<IActionResult> Edit(int id, Product product, Brand brand)
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
                return RedirectToAction("Index");
            }
            ViewBag.Brands = new SelectList(_context.Brand.Distinct(), "Brandid", "Name", product.brand);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.id == id);
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
            var product = await _context.Products.SingleOrDefaultAsync(m => m.id == id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.id == id);
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyTitle(CreateProductViewModel product)
        {
            if(_context.Products.Any(g => g.title.Equals(product.title)))
            {
                return Json(data: $"Name {product.title} is already in use.");
            }
            return Json(data: true);
        }
    }
}
