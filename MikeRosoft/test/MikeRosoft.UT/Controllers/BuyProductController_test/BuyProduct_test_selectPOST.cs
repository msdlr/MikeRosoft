using MikeRosoft.Controllers;
using MikeRosoft.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using MikeRosoft.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using MikeRosoft.UT;
using MikeRosoft.Models.OrderViewModels;
using System.Collections;

namespace MikeRosoft.UT.Controllers.BuyProductController_test
{
    public class Order_SelectItems_test_selectPOST
    {
        private static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("MikeRosoft")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        private DbContextOptions<ApplicationDbContext> _contextOptions;
        private ApplicationDbContext context;
        Microsoft.AspNetCore.Http.DefaultHttpContext ordersContext;

        public Order_SelectItems_test_selectPOST()
        {
            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);

            Brand brand1 = new Brand { Brandid = 1, Name = "Kingston" };
            Brand brand2 = new Brand { Brandid = 2, Name = "Samsung" };


            context.Brand.Add(brand1);
            context.Brand.Add(brand2);


            // Insert seed data into the database using one instance of the context
            context.Products.Add(new Product { id = 4, title = "Memoria RAM", description = "8 GB", brand = brand1, precio = 130, stock = 3 });
            context.Products.Add(new Product { id = 5, title = "Memoria RAM", description = "16 GB", brand = brand2, precio = 130, stock = 0 });
            context.Products.Add(new Product { id = 6, title = "Memoria SSD", description = "32 GB", brand = brand2, precio = 130, stock = 456 });

            context.Users.Add(new User { UserName = "peter@uclm.com", PhoneNumber = "967959595", Email = "peter@uclm.com", Name = "Peter", FirstSurname = "Jackson", SecondSurname = "García" });

            context.SaveChanges();

            
            context.SaveChanges();

            //how to simulate the connection of a user
            System.Security.Principal.GenericIdentity user = new System.Security.Principal.GenericIdentity("namesurname@uclm.com");
            System.Security.Claims.ClaimsPrincipal identity = new System.Security.Claims.ClaimsPrincipal(user);
            ordersContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
            ordersContext.User = identity;
            context.SaveChanges();
        }




        //////////
        // POST //
        //////////

        //IT WORKS
        [Fact]
        public async Task SelectItem_Post_ProductsNotSelected()
        {
            using (context)
            {

                // Arrange
                var controller = new ProductsController(context);
                controller.ControllerContext.HttpContext = ordersContext;

                Brand brand1 = new Brand { Brandid = 1, Name = "Kingston" };
                Brand brand2 = new Brand { Brandid = 2, Name = "Samsung" };
                var brands = new List<Brand> { brand1, brand2 };
                var expectedBrands = new SelectList(brands.Select(b => b.Name).ToList());


                SelectedProductsForBuyViewModel selectedProducts = new SelectedProductsForBuyViewModel();

                IEnumerable<Product> expectedItems = new Product[2] { new Product { id = 4, title = "Memoria RAM", description = "8 GB", brand = brand1, precio = 130, stock = 3 },
                                                  new Product { id = 6, title = "Memoria SDD", description = "32 GB", brand = brand2, precio = 130, stock = 456 }};



                // Act
                var result = controller.SelectProductsForBuy(selectedProducts);

                //Assert
                //Assert
                var viewResult = Assert.IsType<ViewResult>(result); // Check the controller returns a view
                SelectProductsForBuyViewModel model = viewResult.Model as SelectProductsForBuyViewModel;

                Assert.Equal(expectedItems, model.Products, Comparer.Get<Product>((p1, p2) => p1.id == p2.id));
                Assert.Equal(expectedBrands, model.Brands, Comparer.Get<SelectListItem>((p1, p2) => p1.Value == p2.Value));
                // Check that both collections (expected and result returned) have the same elements with the same name
            }
        }


        //IT WORKS
        [Fact]
        public async Task SelectItem_Post_ItemsSelected()
        {
            using (context)
            {

                // Arrange
                var controller = new ProductsController(context);
                controller.ControllerContext.HttpContext = ordersContext;

                String[] ids = new string[1] { "4" };
                SelectedProductsForBuyViewModel items = new SelectedProductsForBuyViewModel { IdsToAdd = ids };

                // Act
                var result = controller.SelectProductsForBuy(items);

                //Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                var currentItems = viewResult.RouteValues.Values.First();
                Assert.Equal(items.IdsToAdd, currentItems);

            }
        }
    }
}