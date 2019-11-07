using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MikeRosoft.Data;
using System;
using System.Threading.Tasks;
using Xunit;
using MikeRosoft.Controllers;
using MikeRosoft.Models;
using MikeRosoft.Models.BanViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace MikeRosoft.UT.Controllers.BansControllers_test
{
    public class Bans_SelectUsersToBan_test
    {
        private DbContextOptions<ApplicationDbContext> _contextOptions;
        private ApplicationDbContext context;
        Microsoft.AspNetCore.Http.DefaultHttpContext banContext;

        public Bans_SelectUsersToBan_test()
        {
            //Initialize the Database
            _contextOptions = Utilities.CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            context.Database.EnsureCreated();

            // Seed the database with test data.
            Utilities.InitializeBanTypes(context);

            Admin admin = new Admin { UserName = "peter@uclm.com", PhoneNumber = "967959595", Email = "peter@uclm.com", Name = "Peter", FirstSurname = "Jackson", SecondSurname = "García", DNI = "66996699K", contractStarting = DateTime.Now, contractEnding = DateTime.Now };
            context.Admins.Add(admin);

            //Create the users of the in-memory database
            User userInDB1 = new User();
            userInDB1.UserName = "elena@uclm.com";
            userInDB1.Email = "elena@uclm.com";
            userInDB1.Name = "Elena";
            userInDB1.FirstSurname = "Navarro";
            userInDB1.SecondSurname = "Martínez";
            userInDB1.DNI = "48484848B";
            userInDB1.Id = "7ba98196-c2bf-4d6e-9d87-bdca85e81a0a";


            User userInDB2 = new User();
            userInDB2.UserName = "test@uclm.com";
            userInDB2.Email = "test@uclm.com";
            userInDB2.Name = "Antonio";
            userInDB2.FirstSurname = "Martinez";
            userInDB2.SecondSurname = "Jimenez";
            userInDB2.DNI = "84172168P";
            userInDB2.Id = "092435f8-ac5b-4c8f-81a9-c7b52a598e02";

            context.Users.Add(userInDB1);
            context.Users.Add(userInDB2);

            context.SaveChanges();

            //how to simulate the connection of a user
            System.Security.Principal.GenericIdentity user = new System.Security.Principal.GenericIdentity("peter@uclm.com");
            System.Security.Claims.ClaimsPrincipal identity = new System.Security.Claims.ClaimsPrincipal(user);
            banContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
            banContext.User = identity;
        }

        /*   GET   */
        [Fact]
        public async Task SelectUsersToBan_GET_NoFilter()
        {
            using (context)
            {
                //Arrange
                var controller = new BansController(context);
                controller.ControllerContext.HttpContext = banContext;

                //Create the expected users
                User expected1 = new User();
                expected1.UserName = "elena@uclm.com";
                expected1.Email = "elena@uclm.com";
                expected1.Name = "Elena";
                expected1.FirstSurname = "Navarro";
                expected1.SecondSurname = "Martínez";
                expected1.DNI = "48484848B";
                expected1.Id = "7ba98196-c2bf-4d6e-9d87-bdca85e81a0a";

                User expected2 = new User();
                expected2.UserName = "test@uclm.com";
                expected2.Email = "test@uclm.com";
                expected2.Name = "Antonio";
                expected2.FirstSurname = "Martinez";
                expected2.SecondSurname = "Jimenez";
                expected2.DNI = "84172168P";
                expected2.Id = "092435f8-ac5b-4c8f-81a9-c7b52a598e02";

                //The user to be shown should be the same
                var expectedUsers = new User[]
                {
                    expected1, expected2
                };


                //Act

                var result = controller.SelectUsersToBan(null, null, null, null);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result); // Check the controller returns a view
                SelectUsersToBanViewModel model = viewResult.Model as SelectUsersToBanViewModel;
                Assert.Equal(expectedUsers, model.Users, Comparer.Get<User>((p1, p2) => p1.Id.Equals(p2.Id)));
            }
        }

        [Fact]
        public async Task SelectUsersToBan_GET_FilterByName()
        {
            using (context)
            {
                //Arrange
                var controller = new BansController(context);
                controller.ControllerContext.HttpContext = banContext;

                //Create the expected user
                User expected1 = new User();
                expected1.UserName = "elena@uclm.com";
                expected1.Email = "elena@uclm.com";
                expected1.Name = "Elena";
                expected1.FirstSurname = "Navarro";
                expected1.SecondSurname = "Martínez";
                expected1.DNI = "48484848B";
                expected1.Id = "7ba98196-c2bf-4d6e-9d87-bdca85e81a0a";

                //The user to be shown should be the same
                var expectedUsers = new User[]
                {
                    expected1
                };


                //Act

                var result = controller.SelectUsersToBan(expected1.Name, null, null, null);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result); // Check the controller returns a view
                SelectUsersToBanViewModel model = viewResult.Model as SelectUsersToBanViewModel;
                Assert.Equal(expectedUsers, model.Users, Comparer.Get<User>((p1, p2) => p1.Id.Equals(p2.Id)));
            }
        }
        [Fact]
        public async Task SelectUsersToBan_GET_FilterByFirstSurame()
        {
            using (context)
            {
                //Arrange
                var controller = new BansController(context);
                controller.ControllerContext.HttpContext = banContext;

                //Create the expected user
                User expected1 = new User();
                expected1.UserName = "elena@uclm.com";
                expected1.Email = "elena@uclm.com";
                expected1.Name = "Elena";
                expected1.FirstSurname = "Navarro";
                expected1.SecondSurname = "Martínez";
                expected1.DNI = "48484848B";
                expected1.Id = "7ba98196-c2bf-4d6e-9d87-bdca85e81a0a";

                //The user to be shown should be the same
                var expectedUsers = new User[]
                {
                    expected1
                };


                //Act

                var result = controller.SelectUsersToBan(null, expected1.FirstSurname, null, null);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result); // Check the controller returns a view
                SelectUsersToBanViewModel model = viewResult.Model as SelectUsersToBanViewModel;
                Assert.Equal(expectedUsers, model.Users, Comparer.Get<User>((p1, p2) => p1.Id.Equals(p2.Id)));
            }
        }
        [Fact]
        public async Task SelectUsersToBan_GET_FilterBySecondSurame()
        {
            using (context)
            {
                //Arrange
                var controller = new BansController(context);
                controller.ControllerContext.HttpContext = banContext;

                //Create the expected user
                User expected1 = new User();
                expected1.UserName = "elena@uclm.com";
                expected1.Email = "elena@uclm.com";
                expected1.Name = "Elena";
                expected1.FirstSurname = "Navarro";
                expected1.SecondSurname = "Martínez";
                expected1.DNI = "48484848B";
                expected1.Id = "7ba98196-c2bf-4d6e-9d87-bdca85e81a0a";

                //The user to be shown should be the same
                var expectedUsers = new User[]
                {
                    expected1
                };


                //Act

                var result = controller.SelectUsersToBan(null, null, expected1.SecondSurname, null);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result); // Check the controller returns a view
                SelectUsersToBanViewModel model = viewResult.Model as SelectUsersToBanViewModel;
                Assert.Equal(expectedUsers, model.Users, Comparer.Get<User>((p1, p2) => p1.Id.Equals(p2.Id)));
            }
        }
        [Fact]
        public async Task SelectUsersToBan_GET_FilterByDNI()
        {
            using (context)
            {
                //Arrange
                var controller = new BansController(context);
                controller.ControllerContext.HttpContext = banContext;

                //Create the expected user
                User expected1 = new User();
                expected1.UserName = "elena@uclm.com";
                expected1.Email = "elena@uclm.com";
                expected1.Name = "Elena";
                expected1.FirstSurname = "Navarro";
                expected1.SecondSurname = "Martínez";
                expected1.DNI = "48484848B";
                expected1.Id = "7ba98196-c2bf-4d6e-9d87-bdca85e81a0a";

                //The user to be shown should be the same
                var expectedUsers = new User[]
                {
                    expected1
                };


                //Act

                var result = controller.SelectUsersToBan(null, null, null, expected1.DNI);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result); // Check the controller returns a view
                SelectUsersToBanViewModel model = viewResult.Model as SelectUsersToBanViewModel;
                Assert.Equal(expectedUsers, model.Users, Comparer.Get<User>((p1, p2) => p1.Id.Equals(p2.Id)));
            }
        }

        /*     POST    */
        
        //Succesful selection
        [Fact]
        public async Task SelectUsersToBan_POST_WithoutUsers()
        {
            using (context)
            {
                //Arrange
                var controller = new BansController(context);
                controller.ControllerContext.HttpContext = banContext;

                User expected1 = new User();
                expected1.UserName = "elena@uclm.com";
                expected1.Email = "elena@uclm.com";
                expected1.Name = "Elena";
                expected1.FirstSurname = "Navarro";
                expected1.SecondSurname = "Martínez";
                expected1.DNI = "48484848B";
                expected1.Id = "7ba98196-c2bf-4d6e-9d87-bdca85e81a0a";

                User expected2 = new User();
                expected2.UserName = "test@uclm.com";
                expected2.Email = "test@uclm.com";
                expected2.Name = "Antonio";
                expected2.FirstSurname = "Martinez";
                expected2.SecondSurname = "Jimenez";
                expected2.DNI = "84172168P";
                expected2.Id = "092435f8-ac5b-4c8f-81a9-c7b52a598e02";

                //The user to be shown should be the same
                var expectedUsers = new User[]
                {
                    expected1, expected2
                };

                //Viewmodel with no users selected
                SelectedUsersToBanViewModel selected = new SelectedUsersToBanViewModel {IdsToAdd= null };

                //Act
                var result = controller.SelectUsersToBan(selected);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result); // Check the controller returns a view
                SelectUsersToBanViewModel model = viewResult.Model as SelectUsersToBanViewModel;


                Assert.Equal(expectedUsers, model.Users, Comparer.Get<User>((p1, p2) => p1.Equals(p2)));
            }
        }

        //Succesful selection
        [Fact]
        public async Task SelectUsersToBan_POST_WithUsers()
        {
            using (context)
            {
                //Arrange
                var controller = new BansController(context);
                controller.ControllerContext.HttpContext = banContext;

                //Viewmodel with the Id of the selected users
                SelectedUsersToBanViewModel selected = new SelectedUsersToBanViewModel { IdsToAdd = new String[] { "7ba98196-c2bf-4d6e-9d87-bdca85e81a0a", "092435f8-ac5b-4c8f-81a9-c7b52a598e02" } };

                //Act
                var result = controller.SelectUsersToBan(selected);

                //Assert
                var viewResult = Assert.IsType<RedirectToActionResult>(result);
                var currentUsers = viewResult.RouteValues.Values.First();
                Assert.Equal(selected.IdsToAdd, currentUsers);
            }
        }
    }
}
