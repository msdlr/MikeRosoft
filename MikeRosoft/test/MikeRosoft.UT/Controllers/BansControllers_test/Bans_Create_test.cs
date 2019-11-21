using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MikeRosoft.Data;
using System;
using System.Threading.Tasks;
using Xunit;
using MikeRosoft.Controllers;
using MikeRosoft.Models;
using MikeRosoft.Models.BanViewModels;
using System.Linq;

namespace MikeRosoft.UT.Controllers.BansControllers_test
{
    public class Bans_Create_test
    {
        private DbContextOptions<ApplicationDbContext> _contextOptions;
        private ApplicationDbContext context;
        Microsoft.AspNetCore.Http.DefaultHttpContext banContext;

        //Admin and users to include in the ban, so that it can be accessed from any method
        Admin admin;
        User userInDB1, userInDB2;

        public Bans_Create_test()
        {
            //Initialize the Database
            _contextOptions = Utilities.CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            context.Database.EnsureCreated();

            // Seed the database with test data.
            Utilities.InitializeBanTypes(context);

            //Simulate admin's connection
            admin = new Admin { UserName = "peter@uclm.com", PhoneNumber = "967959595", Email = "peter@uclm.com", Name = "Peter", FirstSurname = "Jackson", SecondSurname = "García", DNI = "66996699K", contractStarting = DateTime.Now, contractEnding = DateTime.Now };
            context.Admins.Add(admin);

            System.Security.Principal.GenericIdentity user = new System.Security.Principal.GenericIdentity("peter@uclm.com");
            System.Security.Claims.ClaimsPrincipal identity = new System.Security.Claims.ClaimsPrincipal(user);
            banContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
            banContext.User = identity;

            //Create the users of the in-memory database
            userInDB1 = new User();
            userInDB1.UserName = "elena@uclm.com";
            userInDB1.Email = "elena@uclm.com";
            userInDB1.Name = "Elena";
            userInDB1.FirstSurname = "Navarro";
            userInDB1.SecondSurname = "Martínez";
            userInDB1.DNI = "48484848B";
            userInDB1.Id = "7ba98196-c2bf-4d6e-9d87-bdca85e81a0a";


            userInDB2 = new User();
            userInDB2.UserName = "test@uclm.com";
            userInDB2.Email = "test@uclm.com";
            userInDB2.Name = "Antonio";
            userInDB2.FirstSurname = "Martinez";
            userInDB2.SecondSurname = "Jimenez";
            userInDB2.DNI = "84172168P";
            userInDB2.Id = "092435f8-ac5b-4c8f-81a9-c7b52a598e02";

            context.Users.Add(userInDB1);
            context.Users.Add(userInDB2);

            //Save any database changes
            context.SaveChanges();
        }

        [Fact]
        /*   Test case: with all the mandatory data, should succeed   */
        public async Task Create_GET_WithMandatory()
        {
            using (context)
            {
                //Arrange
                var controller = new BansController(context);
                controller.ControllerContext.HttpContext = banContext;

                //Ids selected to call the controller with
                String[] ids = new String[] { userInDB1.Id, userInDB2.Id };
                SelectedUsersToBanViewModel users = new SelectedUsersToBanViewModel() { IdsToAdd = ids };

                //Create expected Ban
                Ban expectedBan = new Ban { ID = 1, BanTime = DateTime.Today, GetAdmin = this.admin };

                //Create the BanTypes for the expected BanForUsers
                var expectedBanTypes = new BanType[]{
                    new Models.BanType { TypeID = 1, TypeName = "Inappropiate behaviour", DefaultDuration = TimeSpan.FromHours(5)},
                    new Models.BanType { TypeID = 2, TypeName = "Unpaid orders", DefaultDuration = TimeSpan.FromDays(7) },
                    new Models.BanType { TypeID = 3, TypeName = "Fraudulent information", DefaultDuration = TimeSpan.FromDays(99) }
                };



                //Act
                //var result = controller.Create(users);

                //Assert
                //ViewResult viewResult = Assert.IsType<ViewResult>(result);
                //CreateBanViewModel currentBan = viewResult.Model as CreateBanViewModel;

                //Assert.Equal(currentBan, expectedBan, Comparer.Get<CreateBanViewModel>((p1, p2) => p1.UserIds.Equals(p2.UserIds)));
            };
        }


        [Fact]
        /*   Test case: with all the mandatory data, should succeed   */
        public async Task Create_GET_WithUsers()
        {
            using (context) {
                /* Arrange */
                var controller = new BansController(context);
                
                /* Act */

                /* Assert */

            }
        }

        [Fact]
        /*   Test case: with all the mandatory data, should succeed   */
        public async Task Create_GET_WithoutUsers()
        {
            using (context)
            {
                /* Arrange */
                var controller = new BansController(context);
                //simulate user's connection
                controller.ControllerContext.HttpContext = banContext;
                SelectedUsersToBanViewModel selectedUsers = new SelectedUsersToBanViewModel {IdsToAdd=null };
                Admin admin = new Admin { UserName = "peter@uclm.com", PhoneNumber = "967959595", Email = "peter@uclm.com", Name = "Peter", FirstSurname = "Jackson", SecondSurname = "García", DNI = "66996699K", contractStarting = DateTime.Now, contractEnding = DateTime.Now };

                CreateBanViewModel expectedModel = new CreateBanViewModel { adminId=admin.Id };

                /* Act */
                var result = controller.Create(selectedUsers);

                /* Assert */
                ViewResult viewResult = Assert.IsType<ViewResult>(result); //check the controller returns a view
                CreateBanViewModel actualBanViewmodel = new CreateBanViewModel();

                var error = viewResult.ViewData.ModelState["NoUsersSelected"].Errors.FirstOrDefault();

                Assert.Equal(actualBanViewmodel, expectedModel, Comparer.Get<CreateBanViewModel>( (p1,p2) => (p1.UserIds==null && p2.UserIds==null ) ));

                Assert.Equal("You should select at least a user to be banned, please", error.ErrorMessage);
            }
        }
    }
}