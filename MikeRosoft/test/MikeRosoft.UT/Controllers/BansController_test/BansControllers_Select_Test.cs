using System;
using System.Collections.Generic;
using System.Text;
using System;
using MikeRosoft.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Xunit;
using MikeRosoft.Models;
using System.Linq;
using MikeRosoft.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace MikeRosoft.UT.Controllers.BansController_test
{
    public class BansControllers_Select_Test
    {
        private DbContextOptions<ApplicationDbContext> _contextOptions;
        private ApplicationDbContext context;
        public BansControllers_Select_Test()
        {
            //Initialize the Database
            _contextOptions = Utilities.CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            context.Database.EnsureCreated();
            // Seed the database with test data.
            //Utilities.InitializeDbMoviesForTests(context);
        }
        [Fact]
        public async Task Index_WithoutFilters()
        {
            using (context)
            {
                //Arrange
                IEnumerable<User> users = context.Users.Include(user => user.BanRecord).Where(user => !user.BanRecord.Any(banforuser => banforuser.End.Date > DateTime.Now)) ;
                var controller = new BansController(context);

                //Act
                //var result = await controller.SelectUsersToBan(null, null, null, null);

                //Assert
                //var viewResult = Assert.IsType<ViewResult>(result);
                //IEnumerable<Genre> model = (result as ViewResult).Model as IEnumerable<Genre>;

            }
        }


    }
}
