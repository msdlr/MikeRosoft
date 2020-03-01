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
    public class BuyProduct_test_create
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

        public BuyProduct_test_create()
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
        public async Task Create_Get_WithSelectedProducts()
        {
            using (context)
            {
                //ARRANGE
                var controller = new ProductsController(context);
                controller.ControllerContext.HttpContext = ordersContext;

                Brand brand1 = new Brand { Brandid = 1, Name = "Kingston" };
                Brand brand2 = new Brand { Brandid = 2, Name = "Samsung" };
                var brands = new List<Brand> { brand1, brand2 };

                String[] ids = new string[1] { "1" };
                SelectedProductsForBuyViewModel selectedProducts = new SelectedProductsForBuyViewModel() { IdsToAdd = ids };

                Product expectedProduct = new Product { id = 1, title = "SSD", description = "8 GB", brand = brand1, precio = 45, stock = 2 };

                User expectedUser = new User { UserName = "elena@uclm.com", Email = "elena@uclm.com", Name = "Elena", FirstSurname = "Navarro", SecondSurname = "Martinez" };

                IList<ProductOrder> expectedProductOrder = new ProductOrder[1] { new ProductOrder { products = expectedProduct } };
                CreateProductsForViewModel expectedOrder = new CreateProductsForViewModel { ProductOrders = expectedProductOrder, UserName = expectedUser.UserName, FirstSurname = expectedUser.FirstSurname, SecondSurname = expectedUser.SecondSurname };


                // Act
                var result = controller.Create(selectedProducts);

                //Assert
                ViewResult viewResult = Assert.IsType<ViewResult>(result);
                CreateProductsForViewModel currentOrder = viewResult.Model as CreateProductsForViewModel;
                Assert.Equal(currentOrder, expectedOrder, Comparer.Get<CreateProductsForViewModel>((p1, p2) => p1.Equals(p2))); 
                Assert.Equal(currentOrder.ProductOrders[0].products, expectedProductOrder[0].products, Comparer.Get<Product>((p1, p2) => p1.Equals(p2)));


            }



        }


        [Fact]
        public async Task Create_Get_WithoutSelectedProducts()
        {
            using (context)
            {
                //ARRANGE
                var controller = new ProductsController(context);
                controller.ControllerContext.HttpContext = ordersContext;

                Brand brand1 = new Brand { Brandid = 1, Name = "Kingston" };
                Brand brand2 = new Brand { Brandid = 2, Name = "Samsung" };
                var brands = new List<Brand> { brand1, brand2 };

                String[] ids = new string[1] { "1" };
                SelectedProductsForBuyViewModel selectedProducts = new SelectedProductsForBuyViewModel() { IdsToAdd = ids };

                Product expectedProduct = new Product { id = 1, title = "SSD", description = "8 GB", brand = brand1, precio = 45, stock = 2 };

                User expectedUser = new User { UserName = "elena@uclm.com", Email = "elena@uclm.com", Name = "Elena", FirstSurname = "Navarro", SecondSurname = "Martinez" };

                //IList<ProductOrder> expectedProductOrder = new ProductOrder[1] { new ProductOrder { products = expectedProduct } };
                CreateProductsForViewModel expectedOrder = new CreateProductsForViewModel { UserName = expectedUser.UserName, FirstSurname = expectedUser.FirstSurname, SecondSurname = expectedUser.SecondSurname };


                // Act
                var result = controller.Create(selectedProducts);

                //Assert
                ViewResult viewResult = Assert.IsType<ViewResult>(result);
                CreateProductsForViewModel currentOrder = viewResult.Model as CreateProductsForViewModel;
                var error = viewResult.ViewData.ModelState[String.Empty].Errors.FirstOrDefault();
                Assert.Equal(currentOrder, expectedOrder, Comparer.Get<CreateProductsForViewModel>((p1, p2) => p1.Equals(p2)));
                Assert.Equal("You have to select at least one item", error.ErrorMessage);


            }
        }


        [Fact]
        public async Task Create_Post_WithoutProducts()
        {
            using (context)
            {
                //ARRANGE
                var controller = new ProductsController(context);
                controller.ControllerContext.HttpContext = ordersContext;

                Brand brand1 = new Brand { Brandid = 1, Name = "Kingston" };
                String[] ids = new string[1] { "1" };
                int[] id_int = new int[1] { 1 };
                SelectedProductsForBuyViewModel selectedProducts = new SelectedProductsForBuyViewModel() { IdsToAdd = ids };
                Product product = new Product { id = 1, title = "SSD", description = "8 GB", brand = brand1, precio = 45, stock = 2 };

                IList<ProductOrder> productOrders = new ProductOrder[1] { new ProductOrder { productId = 1, orderId = 1, products = product } };
                CreateProductsForViewModel viewModel = new CreateProductsForViewModel { UserName = "elena@uclm.com", FirstSurname= "Elena", address="calle falsa", cardExpiration = DateTime.Today, Card ="0984", cardCVC = "6578", productId = id_int };

                Product expectedProduct = new Product { id = 1, title = "SSD", description = "8 GB", brand = brand1, precio = 45, stock = 2 };
                User expectedUser = new User { Id="1", UserName = "elena@uclm.com", Email = "elena@uclm.com", Name = "Elena", FirstSurname = "Navarro", SecondSurname = "Martinez" };

                //IList<ProductOrder> expectedProductOrder = new ProductOrder[1] { new ProductOrder { products = expectedProduct } };
                CreateProductsForViewModel expectedOrder = new CreateProductsForViewModel { UserName = expectedUser.UserName, FirstSurname = expectedUser.FirstSurname, SecondSurname = expectedUser.SecondSurname };


                // Act -- orderviewmodel -- productOrders -- productId, string userId
                var result = controller.CreatePost(viewModel, productOrders, null, userId:expectedUser.Id);

                //Assert
                ViewResult viewResult = Assert.IsType<ViewResult>(result);
                CreateProductsForViewModel currentOrder = viewResult.Model as CreateProductsForViewModel;
                
                var error = viewResult.ViewData.ModelState[String.Empty].Errors.FirstOrDefault();
                Assert.Equal(currentOrder, expectedOrder, Comparer.Get<CreateProductsForViewModel>((p1, p2) => p1.Equals(p2)));
                Assert.Equal("There are not products", error.ErrorMessage);


            }
        }


    }


}




