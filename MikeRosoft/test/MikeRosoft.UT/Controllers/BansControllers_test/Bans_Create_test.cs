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
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MikeRosoft.UT.Controllers.BansControllers_test
{
    public class Bans_Create_test
    {
        private DbContextOptions<ApplicationDbContext> _contextOptions;
        private ApplicationDbContext context;
        Microsoft.AspNetCore.Http.DefaultHttpContext banContext;

        //Admin and users to include in the ban, so that it can be accessed from any method
        Admin admin;
        User expectedUser, userInDB2;

        public Bans_Create_test()
        {
            //Initialize the Database
            _contextOptions = Utilities.CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            context.Database.EnsureCreated();

            // Seed the database with test data.
            Utilities.InitializeBanTypes(context);

            //Simulate admin's connection
            admin = new Admin { UserName = "peter@uclm.com", PhoneNumber = "967959595", Email = "peter@uclm.com", Name = "Peter", FirstSurname = "Jackson", SecondSurname = "García", DNI = "66996699K", contractStarting = DateTime.Now, contractEnding = DateTime.Now, Id="8d11227c-2ae6-42e2-a1a3-a8669fcdd2f3" };
            context.Admins.Add(admin);

            System.Security.Principal.GenericIdentity user = new System.Security.Principal.GenericIdentity("peter@uclm.com");
            System.Security.Claims.ClaimsPrincipal identity = new System.Security.Claims.ClaimsPrincipal(user);
            banContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
            banContext.User = identity;

            //Create the users of the in-memory database
            expectedUser = new User();
            expectedUser.UserName = "elena@uclm.com";
            expectedUser.Email = "elena@uclm.com";
            expectedUser.Name = "Elena";
            expectedUser.FirstSurname = "Navarro";
            expectedUser.SecondSurname = "Martínez";
            expectedUser.DNI = "48484848B";
            expectedUser.Id = "7ba98196-c2bf-4d6e-9d87-bdca85e81a0a";


            userInDB2 = new User();
            userInDB2.UserName = "test@uclm.com";
            userInDB2.Email = "test@uclm.com";
            userInDB2.Name = "Antonio";
            userInDB2.FirstSurname = "Martinez";
            userInDB2.SecondSurname = "Jimenez";
            userInDB2.DNI = "84172168P";
            userInDB2.Id = "092435f8-ac5b-4c8f-81a9-c7b52a598e02";

            context.Users.Add(expectedUser);
            context.Users.Add(userInDB2);

            //Save any database changes
            context.SaveChanges();
        }
        
        [Fact]
        /*   Test case: with all the mandatory data, should succeed   */
        public async Task template()
        {
            using (context) {
                /* Arrange */
                var controller = new BansController(context);
                controller.ControllerContext.HttpContext = banContext;

                /* Act */

                /* Assert */

            }
        }

        [Fact]
        /*   Test case: with all the mandatory data, should succeed   */
        public async Task Create_GET_WithUsers()
        {
            using (context)
            {
                /* Arrange */
                var controller = new BansController(context);
                controller.ControllerContext.HttpContext = banContext;

                //Create the expected user
                expectedUser = new User();
                expectedUser.UserName = "elena@uclm.com";
                expectedUser.Email = "elena@uclm.com";
                expectedUser.Name = "Elena";
                expectedUser.FirstSurname = "Navarro";
                expectedUser.SecondSurname = "Martínez";
                expectedUser.DNI = "48484848B";
                expectedUser.Id = "7ba98196-c2bf-4d6e-9d87-bdca85e81a0a";

                //Create the expected admin
                Admin admin = new Admin { UserName = "peter@uclm.com", PhoneNumber = "967959595", Email = "peter@uclm.com", Name = "Peter", FirstSurname = "Jackson", SecondSurname = "García", DNI = "66996699K", contractStarting = DateTime.Now, contractEnding = DateTime.Now, Id="8d11227c-2ae6-42e2-a1a3-a8669fcdd2f3" };

                //Create the selected viewmodel
                SelectedUsersToBanViewModel selectedModel = new SelectedUsersToBanViewModel { IdsToAdd = new string[]{ expectedUser.Id } };

                //Create expected SelectLists
                var expectedBanTypes = new SelectList(context.BanTypes.Select(g => g).ToList());
                var expectedDurations = new SelectList(context.BanTypes.Select(g => g.DefaultDuration).ToList());

                //Create the expected viewmodel
                CreateBanViewModel expectedBan = new CreateBanViewModel {
                    UserIds = selectedModel.IdsToAdd,
                    adminId = admin.Id,
                    BanTypesAvailable = new SelectList(context.BanTypes.Select(g => g.TypeName).ToList()),
                    defaultDuration = new List<TimeSpan>(context.BanTypes.Select(g => g.DefaultDuration).ToList()),
                    BansForUsers = new List<BanForUser>{ new BanForUser { GetUser = expectedUser } },
                    infoAboutUser= new List<string> { expectedUser.Name + " " + expectedUser.FirstSurname + " (" + expectedUser.DNI + ")" }
                };

                /* Act */
                var result = controller.Create(selectedModel);


                /* Assert */
                ViewResult viewResult = Assert.IsType<ViewResult>(result);
                CreateBanViewModel actualBan = viewResult.Model as CreateBanViewModel;

                //Check the SelectLists
                Assert.Equal(expectedBanTypes, actualBan.BanTypesAvailable.ToList(), Comparer.Get<SelectListItem>((s1, s2) => s1.Value == s2.Value));
                Assert.Equal(expectedDurations, actualBan.BanTypesAvailable.ToList(), Comparer.Get<SelectListItem>((s1, s2) => s1.Value == s2.Value));
                //Check the view is the same
                //Assert.Equal(actualBan, expectedBan);
                Assert.Equal(actualBan, expectedBan, Comparer.Get<CreateBanViewModel>((actual, expected) =>
                    //The only attribute modified in the viewmodel list of BanforUser in GET is the user
                    actual.BansForUsers.First<BanForUser>().GetUser.Equals(expected.BansForUsers.First<BanForUser>().GetUser) &&
                    //Attributes of the ViewModel modified in the GET method
                    actual.adminId.Equals(expected.adminId) &&
                    //actual.infoAboutUser.Equals(expected.infoAboutUser) &&
                    //Attributes that are not set in the GET method
                    actual.banTypeName==null &&
                    expected.banTypeName == null));
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