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
    public class Bans_Details_test
    {
        private DbContextOptions<ApplicationDbContext> _contextOptions;
        private ApplicationDbContext context;
        Microsoft.AspNetCore.Http.DefaultHttpContext banContext;

        //Admin and users to include in the ban, so that it can be accessed from any method
        Admin admin;
        User expectedUser;

        public Bans_Details_test()
        {
            //Initialize the Database
            _contextOptions = Utilities.CreateNewContextOptions();
            context = new ApplicationDbContext(_contextOptions);
            context.Database.EnsureCreated();

            // Seed the database with test data.
            Utilities.InitializeBanTypes(context);

            //Simulate admin's connection
            admin = new Admin { UserName = "peter@uclm.com", PhoneNumber = "967959595", Email = "peter@uclm.com", Name = "Peter", FirstSurname = "Jackson", SecondSurname = "García", DNI = "66996699K", contractStarting = DateTime.Now, contractEnding = DateTime.Now, Id = "8d11227c-2ae6-42e2-a1a3-a8669fcdd2f3" };
            context.Admins.Add(admin);

            System.Security.Principal.GenericIdentity user = new System.Security.Principal.GenericIdentity("peter@uclm.com");
            System.Security.Claims.ClaimsPrincipal identity = new System.Security.Claims.ClaimsPrincipal(user);
            banContext = new Microsoft.AspNetCore.Http.DefaultHttpContext();
            banContext.User = identity;

            //Create the users of the in-memory database
            expectedUser = new User()
            {
                UserName = "elena@uclm.com",
                Email = "elena@uclm.com",
                Name = "Elena",
                FirstSurname = "Navarro",
                SecondSurname = "Martínez",
                DNI = "48484848B",
                Id = "7ba98196-c2bf-4d6e-9d87-bdca85e81a0a"
            };
            context.Users.Add(expectedUser);

            //Data for everything OK
            BanForUser bforUser_OK = new BanForUser {
                Start = DateTime.Now,
                End = DateTime.Now + new TimeSpan(1,0,0,0),
                GetUser = expectedUser,
                GetUserId = expectedUser.Id,
                GetBanTypeID = 3, //"Fraudulent information"
                AdditionalComment ="Fake DNI"

            };
            Ban ban_OK = new Ban {BanTime=DateTime.Now, ID=1,GetAdmin=admin, GetAdminId=admin.Id, GetBanForUsers = new List<BanForUser>()};
            ban_OK.GetBanForUsers.Add(bforUser_OK);

            context.Bans.Add(ban_OK);
            context.BanForUsers.Add(bforUser_OK);

            //No need for data when ban is not found

            //Same if the id is null

            //Data for a ban if admin is not found
            BanForUser bforUser_AdminNotFound = new BanForUser
            {
                Start = DateTime.Now,
                End = DateTime.Now + new TimeSpan(1, 0, 0, 0),
                GetUser = expectedUser,
                GetUserId = expectedUser.Id,
                GetBanTypeID = 3, //"Fraudulent information"
                AdditionalComment = "Fake DNI"

            };
            Ban ban_AdminNotFound = new Ban {BanTime=DateTime.Now, ID=2,GetAdmin=null, GetAdminId="Whatever", GetBanForUsers = new List<BanForUser>()};
            ban_AdminNotFound.GetBanForUsers.Add(bforUser_AdminNotFound);

            context.Bans.Add(ban_AdminNotFound);
            context.BanForUsers.Add(bforUser_AdminNotFound);


            //Data for a ban if user is not found

            BanForUser bforUser_UserNotFound = new BanForUser
            {
                Start = DateTime.Now,
                End = DateTime.Now + new TimeSpan(1, 0, 0, 0),
                GetUser = null,
                GetUserId = "whatever",
                GetBanTypeID = 3, //"Fraudulent information"
                AdditionalComment = "Fake DNI"

            };
            Ban ban_UserNotFound = new Ban { BanTime = DateTime.Now, ID = 3, GetAdmin = admin, GetAdminId = admin.Id, GetBanForUsers = new List<BanForUser>() };
            ban_UserNotFound.GetBanForUsers.Add(bforUser_UserNotFound);

            context.Bans.Add(ban_UserNotFound);
            context.BanForUsers.Add(bforUser_UserNotFound);

            context.SaveChanges();
        }

        //Test cases needed: everything ok, no ban, no id, no admin, no user

        [Fact]
        /*   Test case: with all the mandatory data, should succeed   */
        public async Task Details_EverythingOK()
        {
            // Arrange
            using (context)
            {
                var controller = new BansController(context);
                controller.ControllerContext.HttpContext = banContext;

                BanForUser bforUser_OK = new BanForUser
                {
                    Start = DateTime.Now,
                    End = DateTime.Now + new TimeSpan(1, 0, 0, 0),
                    GetUser = expectedUser,
                    GetUserId = expectedUser.Id,
                    GetBanTypeID = 3, //"Fraudulent information"
                    AdditionalComment = "Fake DNI"

                };
                Ban ban_OK = new Ban { BanTime = DateTime.Now, ID = 1, GetAdmin = admin, GetAdminId = admin.Id, GetBanForUsers = new List<BanForUser>() };
                ban_OK.GetBanForUsers.Add(bforUser_OK);


                // Act
                var result = await controller.Details(1);

                //Assert
                var viewResult = Assert.IsType<ViewResult>(result);

                var model = viewResult.Model as BanDetailsViewModel;
                Assert.Equal(ban_OK, model.ban);

            }
        }

        [Fact]
        public async Task Details_Purchase_NullID()
        {
            // Arrange
            using (context)
            {
                var controller = new BansController(context);
                controller.ControllerContext.HttpContext = banContext;
                // Act
                var result = await controller.Details(null);

                //Assert
                var viewResult = Assert.IsType<NotFoundResult>(result);

            }
        }
        
        [Fact]
        public async Task Details_Purchase_BanNotFound()
        {
            // Arrange
            using (context)
            {
                var controller = new BansController(context);
                controller.ControllerContext.HttpContext = banContext;

                // Act
                var result = await controller.Details(17263);

                //Assert
                var viewResult = Assert.IsType<NotFoundResult>(result);

            }
        }
        
        //[Fact]
        //public async Task Details_Purchase_AdminNotFound()
        //{
        //    // Arrange
        //    using (context)
        //    {
        //        var controller = new BansController(context);
        //        controller.ControllerContext.HttpContext = banContext;

        //        BanForUser bforUser_AdminNotFound = new BanForUser
        //        {
        //            Start = DateTime.Now,
        //            End = DateTime.Now + new TimeSpan(1, 0, 0, 0),
        //            GetUser = expectedUser,
        //            GetUserId = expectedUser.Id,
        //            GetBanTypeID = 3, //"Fraudulent information"
        //            AdditionalComment = "Fake DNI"

        //        };
        //        Ban ban_AdminNotFound = new Ban { BanTime = DateTime.Now, ID = 2, GetAdmin = null, GetAdminId = "Whatever", GetBanForUsers = new List<BanForUser>() };
        //        ban_AdminNotFound.GetBanForUsers.Add(bforUser_AdminNotFound);

        //        // Act
        //        var result = await controller.Details(bforUser_AdminNotFound.GetBanID);

        //        //Assert
        //        var viewResult = Assert.IsType<NotFoundResult>(result);

        //    }
        //}
        
        //[Fact]
        //public async Task Details_Purchase_UserNotFound()
        //{
        //    // Arrange
        //    using (context)
        //    {
        //        var controller = new BansController(context);
        //        controller.ControllerContext.HttpContext = banContext;

        //        BanForUser bforUser_UserNotFound = new BanForUser
        //        {
        //            Start = DateTime.Now,
        //            End = DateTime.Now + new TimeSpan(1, 0, 0, 0),
        //            GetUser = null,
        //            GetUserId = "whatever",
        //            GetBanTypeID = 3, //"Fraudulent information"
        //            AdditionalComment = "Fake DNI"

        //        };
        //        Ban ban_UserNotFound = new Ban { BanTime = DateTime.Now, ID = 3, GetAdmin = admin, GetAdminId = admin.Id, GetBanForUsers = new List<BanForUser>() };
        //        ban_UserNotFound.GetBanForUsers.Add(bforUser_UserNotFound);

        //        // Act
        //        var result = await controller.Details(bforUser_UserNotFound.GetBanID);

        //        //Assert
        //        var viewResult = Assert.IsType<NotFoundResult>(result);

        //    }
        //}
    }
}

