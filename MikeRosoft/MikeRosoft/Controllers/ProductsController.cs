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

            var order = await _context.Order.Include(p => p.ProductOrders).ThenInclude(p => p.products).Include(p => p.user).FirstAsync(p => p.id == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Products/Create
        public IActionResult Create(SelectedProductsForBuyViewModel selectedProducts)
        {

            Product product;
            int id;

            CreateProductsForViewModel order = new CreateProductsForViewModel();
             
            order.ProductOrders  = new List<ProductOrder>();

            if (selectedProducts.IdsToAdd == null)
            {
                ModelState.AddModelError("ProductNotSelected", "You have to select at least one item");
            }

            else
            {
                foreach (string ids in selectedProducts.IdsToAdd)
                {
                    id = int.Parse(ids);
                    product = _context.Products.Include(m => m.brand).FirstOrDefault<Product>(m => m.id.Equals(id));
                    order.ProductOrders.Add(new ProductOrder() { quantity = 1, products = product, productId = product.id });
                }
            }

            User user = _context.Users.OfType<User>().FirstOrDefault<User>(u => u.UserName.Equals(User.Identity.Name));

            order.UserName = user.UserName;
            order.FirstSurname = user.FirstSurname;
            order.SecondSurname = user.SecondSurname;



            return View(order);
        }



        // POST: Products/Create

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(CreateProductsForViewModel orderViewModel, IList<ProductOrder> productOrders, string cardNumber)
        {
            //Movie movie;
            // customer;

            /*
             * 
             * MOVIE = PRODUCT
             * CUSTOMER = USER
             *                   
             */
            Product product;
            User user;
            Order order = new Order();
            order.totalprice = 0;
            order.ProductOrders = new List<ProductOrder>();
            ModelState.Clear();
            foreach (ProductOrder po in productOrders)
            {
                product = await _context.Products.FirstOrDefaultAsync<Product>(m => m.id == po.products.id);
                if (product.stock < po.quantity)
                {

                    ModelState.AddModelError("NoEnoughMovies", $"There are no enough movies titled {product.title}, please select less or equal than {product.stock}");
                    orderViewModel.ProductOrders = productOrders;
                }
                else
                {
                    if (po.quantity > 0)
                    {
                        product.stock = product.stock - po.quantity;
                        po.products = product;
                        po.orders = order;
                        order.totalprice += po.quantity * product.precio;
                        order.ProductOrders.Add(po);
                    }
                }
            }
            user = await _context.Users.OfType<User>().FirstOrDefaultAsync<User>(u => u.UserName.Equals(User.Identity.Name));

            if (ModelState.ErrorCount > 0)
            {
                orderViewModel.UserName = user.Name;
                orderViewModel.FirstSurname = user.FirstSurname;
                orderViewModel.SecondSurname = user.SecondSurname;
                return View(orderViewModel);
            }
            if (order.totalprice == 0)
            {
                orderViewModel.UserName = user.Name;
                orderViewModel.FirstSurname = user.FirstSurname;
                orderViewModel.SecondSurname = user.SecondSurname;
                ModelState.AddModelError("MovieForPurchase0", $"Please select at least a movie to be bought or cancel your purchase");
                orderViewModel.ProductOrders = productOrders;
                return View(orderViewModel);
            }


            //YO NO TENGO PARA ELEGIR, SIEMPRE ES CREDIT CARD

            order.user = user;
            order.orderDate = DateTime.Now;

            order.Card = orderViewModel.Card;
            order.cardCVC = orderViewModel.cardCVC;
            order.cardExpiration = orderViewModel.cardExpiration;


            order.deliveryAddress = orderViewModel.address;
            _context.Add(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = order.id });
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

            selectProducts.Brands = new SelectList(_context.Brand.Select(p => p.Name).ToList());

            //Añade a products todos los productos que están en la base de datos cuyo stock es mayor que 0 
            selectProducts.Products = _context.Products.Where(p => p.stock > 0);

            //Para filtrar por nombre
            if (titleSelected != null)
                selectProducts.Products = selectProducts.Products.Where(p => p.title.Contains(titleSelected));

            //Para filtrar por brand
            if (brandSelected != null)
                selectProducts.Products = selectProducts.Products.Where(p => p.brand.Name.Contains(brandSelected));

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