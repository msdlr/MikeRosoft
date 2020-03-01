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
    public class Recommendation_Index_test
    {

        private DbContextOptions<ApplicationDbContext> _ContextOptions;
        private ApplicationDbContext context;
        Microsoft.AspNetCore.Http.DefaultHttpContext recommendationContext;

        public Recommendation_Index_test(){
            //Initialize the Database
            _ContextOptions = Utilities.CreateNewContextOptions();
            context = new ApplicationDbContext(_ContextOptions);
            context.Database.EnsureCreated();

            //Seed the database with test data
           /* Utilities.InitializeDbAdminForTests(context);
            Utilities.InitializeDbBrandsForTests(context);*/
            //Utilities.InitializeDbProductsForTest(context);
            Utilities.InitializeDbRecommendationForTests(context);
            context.SaveChanges();

            //how to simulate the connection of a user
            System.Security.Principal.GenericIdentity user = new System.Security.Principal.GenericIdentity("peter@uclm.com");
            System.Security.Claims.ClaimsPrincipal identity = new System.Security.Claims.ClaimsPrincipal(user);
            recommendationContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
            recommendationContext.User = identity;

        }

        [Fact]
        public async Task Index_Get()
        {
            using (context)
            {
                int i;
                //IEnumerable<MikeRosoft.Models.Recommendation> expectedRecommendation;
                // IEnumerable<MikeRosoft.Models.Recommendation> expectedRecommendation = new List<Recommendation> { Utilities.Recommendation };
                var expectedRecommendation = new List<Recommendation> { Utilities.Recommendation };
                var controller = new RecommendationsController(context);
                controller.ControllerContext.HttpContext = recommendationContext;

                //Act
                var result = controller.Index();

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);

                // IEnumerable<Recommendation> model = viewResult.Model as IEnumerable<Recommendation>;
                //var currentRecommendation = model.ToList();
                List<Recommendation> model = viewResult.Model as List<Recommendation>;

                Assert.Equal(expectedRecommendation.Count(), model.Count());

                for (i = 0; i < model.Count(); i++)
                {
                    Assert.Equal(expectedRecommendation[i], model[i]);
                }
            }

        }

    }
}
