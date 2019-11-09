using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MikeRosoft.Models;
using MikeRosoft.Data;
using MikeRosoft.Models.RecommendationViewModels;
using MikeRosoft.Controllers;

namespace MikeRosoft.UT.Controllers.Recommendation
{
    class RecommendationsController_SelectProductsForRecommendation_test
    {
        private DbContextOptions<ApplicationDbContext> _contextOptions;
        private ApplicationDbContext context;

        public RecommendationsController_SelectProductsForRecommendation_test()
        {
            //Initialize the Database
            _contextOptions = Utilities.CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            context.Database.EnsureCreated();
        }
    }
}
