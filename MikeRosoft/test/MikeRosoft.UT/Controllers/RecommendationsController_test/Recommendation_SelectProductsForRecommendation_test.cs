using MikeRosoft.Controllers;
using MikeRosoft.Data;
using MikeRosoft.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using MikeRosoft.Models.RecommendationViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using MikeRosoft.UT.Controllers;

namespace MikeRosoft.UT.Controllers.RecommendationsController_test
{
    public class Recommendation_SelectProductsForRecommendation_test
    {
        private DbContextOptions<ApplicationDbContext> _contextOptions;
        private ApplicationDbContext context;
        Microsoft.AspNetCore.Http.DefaultHttpContext recommendationContext;

        public Recommendation_SelectProductsForRecommendation_test()
        {
            //Initialize the Database
            _contextOptions = Utilities.CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            context.Database.EnsureCreated();

            //Seed the database with test data
            Utilities.InitializeDbProductsForTest(context);

            context.Admins.Add(new Admin { contractEnding = new DateTime(2020, 10, 10), contractStarting = new DateTime(2018, 12, 6), UserName = "peter@uclm.com", PhoneNumber = "967959595", Email = "peter@uclm.com", Name = "Peter", FirstSurname = "Jackson", SecondSurname = "García", DNI = "12345678D" });
            context.SaveChanges();

            //how to simulate the connection of a user
            System.Security.Principal.GenericIdentity user = new System.Security.Principal.GenericIdentity("peter@uclm.com");
            System.Security.Claims.ClaimsPrincipal identity = new System.Security.Claims.ClaimsPrincipal(user);
            recommendationContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
            recommendationContext.User = identity;
        }

        [Fact]
        public async Task SelectProduct_Get_WithoutAnyFilter()
        {
            using (context)
            {
                //Arrage
                var controller = new RecommendationsController(context);
                controller.ControllerContext.HttpContext = recommendationContext;
                Brand brand = new Brand { Name = "HP" };
                var brands = new List<Brand> { brand };
                var expectedBrands = new SelectList(brands.Select(g => g.Name).ToList());
                var expectedProducts = new Product[3] { new Product { id = 1, title = "Gamer Mouse", description = "1Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 20, stock = 100, rate = 4 }, 
                                                        new Product { id = 2, title = "Dark Keyboard", description = "2Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 25, stock = 50, rate = 3 }, 
                                                        new Product { id = 3, title = "Silence Mouse", description = "3Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 30, stock = 89, rate = 5 }};

                //Act
                var result = controller.SelectProductsForRecommendation(null, null, -1, -1);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);//Check the controller returns a view
                SelectProductsForRecommendationViewModel model = viewResult.Model as SelectProductsForRecommendationViewModel;
                Assert.Equal(expectedProducts, model.Products, Comparer.Get<Product>((p1, p2) => p1.title == p2.title && p1.description == p2.description && p1.precio == p2.precio && p1.rate == p2.rate && p1.stock == p2.stock));
                Assert.Equal(expectedBrands, model.Brands, Comparer.Get<SelectListItem>((s1, s2) => s1.Value == s2.Value));
                //Check that both collections (expected and result returned) have the same elements with the same name 
            }
        }

        [Fact]
        public async Task SelectProduct_Get_WithFilterProductTitle()
        {
            using (context)
            {
                //Arrange
                var controller = new RecommendationsController(context);
                controller.ControllerContext.HttpContext = recommendationContext;
                Brand brand = new Brand { Name = "HP" };
                var brands = new List<Brand> { brand };
                var expectedBrands = new SelectList(brands.Select(g => g.Name).ToList());
                var expectedProducts = new Product[2] { new Product { id = 1, title = "Gamer Mouse", description = "1Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 20, stock = 100, rate = 4 },
                                                        new Product { id = 3, title = "Silence Mouse", description = "3Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 30, stock = 89, rate = 5 }};

                //Act
                var result = controller.SelectProductsForRecommendation("Mouse", null, -1, -1);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                SelectProductsForRecommendationViewModel model = viewResult.Model as SelectProductsForRecommendationViewModel;
                Assert.Equal(expectedProducts, model.Products, Comparer.Get<Product>((p1, p2) => p1.title == p2.title && p1.description == p2.description && p1.precio == p2.precio && p1.rate == p2.rate && p1.stock == p2.stock));
                Assert.Equal(expectedBrands, model.Brands, Comparer.Get<SelectListItem>((s1, s2) => s1.Value == s2.Value));
            }
        }

        [Fact]
        public async Task SelectProduct_Get_WithFilterProductPrice()
        {
            using (context)
            {
                //Arrange
                var controller = new RecommendationsController(context);
                controller.ControllerContext.HttpContext = recommendationContext;
                Brand brand = new Brand { Name = "HP" };
                var brands = new List<Brand> { brand };
                var expectedBrands = new SelectList(brands.Select(g => g.Name).ToList());
                var expectedProducts = new Product[1] { new Product { id = 3, title = "Silence Mouse", description = "3Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 30, stock = 89, rate = 5 } };

                //Act
                var result = controller.SelectProductsForRecommendation(null, null, 26, -1);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                SelectProductsForRecommendationViewModel model = viewResult.Model as SelectProductsForRecommendationViewModel;
                Assert.Equal(expectedProducts, model.Products, Comparer.Get<Product>((p1, p2) => p1.title == p2.title && p1.description == p2.description && p1.precio == p2.precio && p1.rate == p2.rate && p1.stock == p2.stock));
                Assert.Equal(expectedBrands, model.Brands, Comparer.Get<SelectListItem>((s1, s2) => s1.Value == s2.Value));
            }
        }

        [Fact]
        public async Task SelectProduct_Get_WithFilterProductRate()
        {
            using (context)
            {
                //Arrange
                var controller = new RecommendationsController(context);
                controller.ControllerContext.HttpContext = recommendationContext;
                Brand brand = new Brand { Name = "HP" };
                var brands = new List<Brand> { brand };
                var expectedBrands = new SelectList(brands.Select(g => g.Name).ToList());
                var expectedProducts = new Product[3] { new Product { id = 1, title = "Gamer Mouse", description = "1Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 20, stock = 100, rate = 4 },
                                                        new Product { id = 2, title = "Dark Keyboard", description = "2Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 25, stock = 50, rate = 3 },
                                                        new Product { id = 3, title = "Silence Mouse", description = "3Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 30, stock = 89, rate = 5 }};

                //Act
                var result = controller.SelectProductsForRecommendation(null, null);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                SelectProductsForRecommendationViewModel model = viewResult.Model as SelectProductsForRecommendationViewModel;
                Assert.Equal(expectedProducts, model.Products, Comparer.Get<Product>((p1, p2) => p1.title == p2.title && p1.description == p2.description && p1.precio == p2.precio && p1.rate == p2.rate && p1.stock == p2.stock));
                Assert.Equal(expectedBrands, model.Brands, Comparer.Get<SelectListItem>((s1, s2) => s1.Value == s2.Value));
            }
        }

        [Fact]
        public async Task SelectProduct_Get_WithFilterProductBrand()
        {
            using (context)
            {
                //Arrange
                var controller = new RecommendationsController(context);
                controller.ControllerContext.HttpContext = recommendationContext;
                Brand brand = new Brand { Name = "HP" };
                var brands = new List<Brand> { brand };
                var expectedBrands = new SelectList(brands.Select(g => g.Name).ToList());
                var expectedProducts = new Product[3] { new Product { id = 1, title = "Gamer Mouse", description = "1Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 20, stock = 100, rate = 4 },
                                                        new Product { id = 2, title = "Dark Keyboard", description = "2Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 25, stock = 50, rate = 3 },
                                                        new Product { id = 3, title = "Silence Mouse", description = "3Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 30, stock = 89, rate = 5 }};

                //Act
                var result = controller.SelectProductsForRecommendation(null, "HP", -1, -1);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                SelectProductsForRecommendationViewModel model = viewResult.Model as SelectProductsForRecommendationViewModel;
                Assert.Equal(expectedProducts, model.Products, Comparer.Get<Product>((p1, p2) => p1.title == p2.title && p1.description == p2.description && p1.precio == p2.precio && p1.rate == p2.rate && p1.stock == p2.stock));
                Assert.Equal(expectedBrands, model.Brands, Comparer.Get<SelectListItem>((s1, s2) => s1.Value == s2.Value));
            }
        }

        [Fact]
        public async Task SelectProduct_Post_ProductsNotSelected()
        {
            using (context)
            {
                //Arrange
                var controller = new RecommendationsController(context);
                controller.ControllerContext.HttpContext = recommendationContext;
                Brand brand = new Brand { Name = "HP" };
                var brands = new List<Brand> { brand };
                var expectedBrands = new SelectList(brands.Select(g => g.Name).ToList());
                var expectedProducts = new Product[3] { new Product { id = 1, title = "Gamer Mouse", description = "1Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 20, stock = 100, rate = 4 },
                                                        new Product { id = 2, title = "Dark Keyboard", description = "2Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 25, stock = 50, rate = 3 },
                                                        new Product { id = 3, title = "Silence Mouse", description = "3Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 30, stock = 89, rate = 5 }};
                SelectedProductsForRecommendationViewModel selected = new SelectedProductsForRecommendationViewModel { IdsToAdd = null };
                
                //Act
                var result = controller.SelectProductsForRecommendation(selected);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
                SelectProductsForRecommendationViewModel model = viewResult.Model as SelectProductsForRecommendationViewModel;
                Assert.Equal(expectedProducts, model.Products, Comparer.Get<Product>((p1, p2) => p1.title == p2.title && p1.description == p2.description && p1.precio == p2.precio && p1.rate == p2.rate && p1.stock == p2.stock));
                Assert.Equal(expectedBrands, model.Brands, Comparer.Get<SelectListItem>((s1, s2) => s1.Value == s2.Value));
            }
        }

        [Fact]
        public async Task SelectProduct_Post_ProductsSelected()
        {
            using (context)
            {
                //Arrange
                var controller = new RecommendationsController(context);
                controller.ControllerContext.HttpContext = recommendationContext;
                String[] ids = new string[1] { "1" };
                SelectedProductsForRecommendationViewModel products = new SelectedProductsForRecommendationViewModel { IdsToAdd = ids };

                //Act
                var result = controller.SelectProductsForRecommendation(products);

                //Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                var currentProducts = viewResult.RouteValues.Values.First();
                Assert.Equal(products.IdsToAdd, currentProducts);
            }
        }
    }
}
