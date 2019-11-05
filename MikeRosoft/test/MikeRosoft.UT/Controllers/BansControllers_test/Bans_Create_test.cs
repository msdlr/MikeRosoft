using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MikeRosoft.Data;
using System;
using System.Threading.Tasks;
using Xunit;
using MikeRosoft.Controllers;
using MikeRosoft.Models;
using MikeRosoft.Models.BanViewModels;

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
                String[] ids = new String[] { userInDB1.Id , userInDB2.Id };
                SelectedUsersToBanViewModel users = new SelectedUsersToBanViewModel() { IdsToAdd=ids };

                //Create expected Ban
                Ban expectedBan = new Ban { ID = 1, BanTime = DateTime.Today, GetAdmin=this.admin};

                //Create the BanTypes for the expected BanForUsers
                var expectedBanTypes = new BanType[]{
                   new BanType { TypeID = 1, TypeName = "Inappropiate behaviour", DefaultDuration = TimeSpan.FromHours(5)},
                   new BanType { TypeID = 2 , TypeName = "Unpaid orders", DefaultDuration = TimeSpan.FromDays(7)}
                };

                //Create BanForUser
                var expectedBanForUsers = new BanForUser[]
                {
                    new BanForUser{ID=1, GetBan = expectedBan, Start = expectedBan.BanTime, GetUser=userInDB1, GetBanType= expectedBanTypes[0], 
                        End = DateTime.Now+ expectedBanTypes[0].DefaultDuration},
                    new BanForUser{ID=2, GetBan = expectedBan, Start = expectedBan.BanTime, GetUser=userInDB2, GetBanType= expectedBanTypes[1],
                        End = DateTime.Now+ expectedBanTypes[1].DefaultDuration, AdditionalComment="Last 3 orders not paid"}
                };
                //expectedBan.GetBanForUsers.Add(expectedBanForUsers[0]);
                //expectedBan.GetBanForUsers.Add(expectedBanForUsers[1]);

                //Create the expected model that encapsulates the data
                CreateBanViewModel expectedBanModel = new CreateBanViewModel
                {
                    UserIds = ids,
                    banTypeName = new String[] { expectedBanTypes[0].TypeName, expectedBanTypes[1].TypeName },
                    AdditionalComment = new String[] { expectedBanForUsers[0].AdditionalComment, expectedBanForUsers[1].AdditionalComment },
                    GetBanTypeID = new int[] { expectedBanTypes[0].TypeID, expectedBanTypes[1].TypeID },
                    EndDate = new DateTime[] { expectedBanForUsers[0].End, expectedBanForUsers[1].End },
                    StartDate = new DateTime[] { expectedBanForUsers[0].Start, expectedBanForUsers[1].Start},
                };

                //Act
                var result = controller.Create(users);

                //Assert
                ViewResult viewResult = Assert.IsType<ViewResult>(result);
                CreateBanViewModel currentBan = viewResult.Model as CreateBanViewModel;

                Assert.Equal(currentBan, expectedBanModel, Comparer.Get<CreateBanViewModel>((p1, p2) => p1.Equals(p2)));
            }
        }
    }
}
