using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MikeRosoft.Data;
using MikeRosoft.Controllers;
using MikeRosoft.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MikeRosoft.UT.Controllers.BuyProductController_test
{

    public class BuyProduct_test_details
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

            builder.UseInMemoryDatabase("MikeRosoft").UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }


        private DbContextOptions<ApplicationDbContext> _contextOptions;
        private ApplicationDbContext context;
        Microsoft.AspNetCore.Http.DefaultHttpContext ordersContext;

        public BuyProduct_test_details()
        {

            _contextOptions = CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            // Insert seed data into the database using one instance of the context

            Brand brand1 = new Brand { Brandid = 1, Name = "Kingston" };
            Brand brand2 = new Brand { Brandid = 2, Name = "Samsung" };


            context.Brand.Add(brand1);
            context.Brand.Add(brand2);

            context.Products.Add(new Product { id = 1, title = "Memoria RAM", description = "8 GB", brand = brand1, precio = 130, stock = 3 });
            context.Products.Add(new Product { id = 2, title = "Memoria RAM", description = "16 GB", brand = brand2, precio = 130, stock = 0 });

            context.Users.Add(new User { UserName = "peter@uclm.com", PhoneNumber = "967959595", Email = "peter@uclm.com", Name = "Peter", FirstSurname = "Jackson", SecondSurname = "García" });

            context.SaveChanges();

            //how to simulate the connection of a user
            System.Security.Principal.GenericIdentity user = new System.Security.Principal.GenericIdentity("peter@uclm.com");
            System.Security.Claims.ClaimsPrincipal identity = new System.Security.Claims.ClaimsPrincipal(user);
            ordersContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
            ordersContext.User = identity;
            context.SaveChanges();


        }

        [Fact]
        public async Task Details_withoutId()
        {
            // Arrange
            using (context)
            {
                var controller = new ProductsController(context);
                controller.ControllerContext.HttpContext = ordersContext;

                // Act
                var result = await controller.Details(null);

                //Assert
                var viewResult = Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact]
        public async Task Details_Order_notfound()
        {
            // Arrange
            using (context)
            {
                var controller = new ProductsController(context);
                controller.ControllerContext.HttpContext = ordersContext;
                var id = context.Order.Last().id + 1;

                // Act
                var result = await controller.Details(id);

                //Assert
                var viewResult = Assert.IsType<NotFoundResult>(result);

            }
        }

        [Fact]
        public async Task Details_Purchase_found()
        {
            // Arrange
            using (context)
            {
                Brand brand1 = new Brand { Brandid = 1, Name = "Kingston" };

                //Creating a list of items that are going to compose the order
                var product = new List<Product>();
                product.Add(new Product { id = 1, title = "Memoria RAM", description = "8 GB", brand = brand1, precio = 130, stock = 3 });
                
                var user = new User { UserName = "elena@uclm.es", Id = "1"};

                var expectedOrder = new Order { id=1, Card="82734", cardCVC="908", userId="1" };

                var orderitems = new List<ProductOrder>();

                int[] ids_int = new int[1] { 1 };

                //To add the items into the order => foreach
                foreach (var item in product)
                    orderitems.Add(new ProductOrder { orderId =1, productId=});

                expectedOrder.ProductOrders = orderitems;

                context.SaveChanges();

                var controller = new ProductsController(context);
                controller.ControllerContext.HttpContext = ordersContext;
                var id = context.Order.Last().id;

                // Act
                var result = await controller.Details(id);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);

                var model = viewResult.Model as Order;

                Assert.Equal(model, expectedOrder, Comparer.Get<Order>((p1, p2) => p1.Equals(p2)));
                Assert.Equal(model.ProductOrders, expectedOrder.ProductOrders, Comparer.Get<ProductOrder>((p1, p2) => p1.Equals(p2)));
            }
        }

    }
}
