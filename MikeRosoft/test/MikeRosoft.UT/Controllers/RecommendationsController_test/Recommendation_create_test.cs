using MikeRosoft.Controllers;
using MikeRosoft.Data;
using MikeRosoft.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using MikeRosoft.Models.RecommendationViewModels;
using MikeRosoft.UT.Controllers;

namespace MikeRosoft.UT.Controllers.RecommendationsController_test
{
    public class Recommendation_create_test
    {
        private DbContextOptions<ApplicationDbContext> _contextOptions;
        private ApplicationDbContext context;
        Microsoft.AspNetCore.Http.DefaultHttpContext recommendationContext;

        public Recommendation_create_test()
        {
            //Initialize the Database
            _contextOptions = Utilities.CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            context.Database.EnsureCreated();

            //Seed the database with test data
            Utilities.InitializeDbProductsForTest(context);
            Utilities.InitializeDbAdminForTests(context);
            Utilities.InitializeDbBrandsForTests(context);
            //Utilities.InitializeDbRecommendationForTests(context);
            //context.Admins.Add(new Admin { contractEnding = new DateTime(2020, 10, 10), contractStarting = new DateTime(2018, 12, 6), UserName = "peter@uclm.com", PhoneNumber = "967959595", Email = "peter@uclm.com", Name = "Peter", FirstSurname = "Jackson", SecondSurname = "García", DNI = "12345678D" });
            //context.SaveChanges();
            context.SaveChanges();


            //how to simulate the connection of a user
            System.Security.Principal.GenericIdentity user = new System.Security.Principal.GenericIdentity("peter@uclm.com");
            System.Security.Claims.ClaimsPrincipal identify = new System.Security.Claims.ClaimsPrincipal(user);
            recommendationContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
            recommendationContext.User = identify;

        }

        [Fact]
        public async Task Create_Get_WithSelectedProducts()
        {
            using (context)
            {
                //Arrange
                var controller = new RecommendationsController(context);
                //simulate user's connection
                controller.ControllerContext.HttpContext = recommendationContext;

                String[] ids = new string[1] { "1" };
                var brand = new Brand { Name = "HP" };
                SelectedProductsForRecommendationViewModel products = new SelectedProductsForRecommendationViewModel() { IdsToAdd = ids};
                Product expectedProduct = new Product { id = 1, title = "Gamer Mouse", description = "1Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 20, stock = 100, rate = 4 };
                Admin expectedAdmin = new Admin { UserName = "peter@uclm.com", PhoneNumber = "967959595", Email = "peter@uclm.com", Name = "Peter", FirstSurname = "Jackson", SecondSurname = "García", DNI = "12345678D" };

                IList<ProductRecommend> expectedProductRecommend = new ProductRecommend[1] { new ProductRecommend { Product = expectedProduct } };
                RecommendationCreateViewModel expectedRecommendation = new RecommendationCreateViewModel { ProductRecommendations = expectedProductRecommend, Name = expectedAdmin.Name, FirstSurname = expectedAdmin.FirstSurname, SecondSurname = expectedAdmin.SecondSurname, DNI = expectedAdmin.DNI};

                //Act
                var result = controller.Create(products);

                //Assert
                ViewResult viewResult = Assert.IsType<ViewResult>(result);
                RecommendationCreateViewModel currentRecommendation = viewResult.Model as RecommendationCreateViewModel;

                Assert.Equal(expectedRecommendation, currentRecommendation, Comparer.Get<RecommendationCreateViewModel>((p1, p2) => p1.NameRec == p2.NameRec && p1.DNI == p2.DNI && p1.Description == p2.Description ));

                //Assert.Equal(currentRecommendation, expectedRecommendation);
            }
        }

        /*[Fact]

        public async Task Create_Get_WithoutProduct()
        {
            using (context)
            {
                //Arrange
                var controller = new RecommendationsController(context);
                //simulate user's connection
                controller.ControllerContext.HttpContext = recommendationContext;
                SelectedProductsForRecommendationViewModel products = new SelectedProductsForRecommendationViewModel();
                Admin expectedAdmin = new Admin {  UserName = "peter@uclm.com", PhoneNumber = "967959595", Email = "peter@uclm.com", Name = "Peter", FirstSurname = "Jackson", SecondSurname = "García", DNI = "12345678D" };

                RecommendationCreateViewModel expectedRecommendation = new RecommendationCreateViewModel { Name = expectedAdmin.Name, FirstSurname = expectedAdmin.FirstSurname, SecondSurname = expectedAdmin.SecondSurname, DNI = expectedAdmin.DNI };

                //Act
                var result = controller.Create(products);

                //Assert
                ViewResult viewResult = Assert.IsType<ViewResult>(result);
                RecommendationCreateViewModel currentRecommendation = viewResult.Model as RecommendationCreateViewModel;
                var error = viewResult.ViewData.ModelState["ProductNoSelected"].Errors.FirstOrDefault();
                Assert.Equal(currentRecommendation, expectedRecommendation, Comparer.Get<RecommendationCreateViewModel>((p1, p2) => p1.Name == p2.Name && p1.FirstSurname == p2.FirstSurname && p1.SecondSurname == p2.SecondSurname));
                //Assert.Equal("You should select at least a Product to be recommend, please", error.ErrorMessage);

            }
        }*/
    }
}
