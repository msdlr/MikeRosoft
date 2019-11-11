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
using MikeRosoft.Models.ProductViewModels;
using MikeRosoft.Controllers;
using MikeRosoft.UT.Controllers;

namespace MikeRosoft.UT.Controllers.BrandsController_test
{
    class BrandsController_Create_test
    {
        /*private DbContextOptions<ApplicationDbContext> _contextOptions;
        private ApplicationDbContext context;

        public BrandsController_Create_test()
        {
            //Initialize the Database
            _contextOptions = Utilities.CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            context.Database.EnsureCreated();

            //Seed the database with test data
            Utilities.InitializeDbBrandsForTests(context);
        }

        [Fact]
        public async Task Create_EmptyArguments()
        {
            using (context)
            {
                //Arrange
                var controller = new BrandsController(context);

                //Act
                var result = controller.Create();

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);
            }
        }

        [Fact]
        public async Task Create_ModelIsNOTValid()
        {
            //Arrange
            using (context)
            {
                IEnumerable<Brand>
            }
        }*/

    }
        
}
