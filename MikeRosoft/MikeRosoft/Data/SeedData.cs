using MikeRosoft.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using MikeRosoft.Data;

namespace AppForMovies.Data
{
    public static class SeedData
    {
        //public static void Initialize(UserManager<ApplicationUser> userManager,
        //            RoleManager<IdentityRole> roleManager)
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var role = serviceProvider.GetRequiredService(typeof(RoleManager<IdentityRole>));
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();


            List<string> rolesNames = new List<string> { "Admin", "User" };

            SeedRoles(roleManager, rolesNames);
            SeedUsers(userManager, rolesNames);
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager, List<string> roles)
        {

            foreach (string roleName in roles)
            {
                //it checks such role does not exist in the database 
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    IdentityRole role = new IdentityRole();
                    role.Name = roleName;
                    role.NormalizedName = roleName;
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                }
            }

        }

        public static void SeedUsers(UserManager<IdentityUser> userManager, List<string> roles)
        {
            //first, it checks the user does not already exist in the DB
            
            if (userManager.FindByNameAsync("ms@uclm.es").Result == null)
            {
                ApplicationUser user = new Admin();
                user.UserName = "ms@uclm.es";
                user.Email = "ms@uclm.es";
                user.Name = "Miguel";
                user.FirstSurname = "Sanchez";
                user.SecondSurname = "De la Rosa";

                IdentityResult result = userManager.CreateAsync(user, "Password1234%").Result;

                if (result.Succeeded)
                {
                    //administrator role
                    userManager.AddToRoleAsync(user, roles[0]).Wait();
                    user.EmailConfirmed = true;
                }
            }

            if (userManager.FindByNameAsync("elena@uclm.com").Result == null)
            {
                ApplicationUser user = new User();
                user.UserName = "elena@uclm.com";
                user.Email = "elena@uclm.com";
                user.Name = "Elena";
                user.FirstSurname = "Navarro";
                user.SecondSurname = "Martínez";
                user.DNI = "48484848B";

                IdentityResult result = userManager.CreateAsync(user, "Password1234%").Result;

                if (result.Succeeded)
                {
                    //User role
                    userManager.AddToRoleAsync(user, roles[1]).Wait();
                    user.EmailConfirmed = true;
                }
            }

            if (userManager.FindByNameAsync("namesurname@uclm.com").Result == null)
            {
                ApplicationUser user = new User();
                user.UserName = "namesurname@uclm.com";
                user.Email = "namesurname@uclm.com";
                user.Name = "Name";
                user.FirstSurname = "Surname";
                user.SecondSurname = "SurSurname";
                user.DNI = "48484848C";

                IdentityResult result = userManager.CreateAsync(user, "APassword1234%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, roles[1]).Wait();
                    user.EmailConfirmed = true;
                }
            }

            if (userManager.FindByNameAsync("ABC@uclm.com").Result == null)
            {
                ApplicationUser user = new User();
                user.UserName = "ABC@uclm.com";
                user.Email = "ABC@uclm.com";
                user.Name = "A";
                user.FirstSurname = "B";
                user.SecondSurname = "C";
                user.DNI = "12345678J";

                IdentityResult result = userManager.CreateAsync(user, "APassword1234%").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, roles[1]).Wait();
                    user.EmailConfirmed = true;
                }
            }
        }
    }
}


