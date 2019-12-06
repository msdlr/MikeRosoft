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

            //how to simulate the connection of a user
            System.Security.Principal.GenericIdentity user = new System.Security.Principal.GenericIdentity("peter@uclm.com");
            System.Security.Claims.ClaimsPrincipal identify = new System.Security.Claims.ClaimsPrincipal(user);
            recommendationContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
            recommendationContext.User = identify;

        }
    }
}
