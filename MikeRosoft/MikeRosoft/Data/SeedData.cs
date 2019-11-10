using MikeRosoft.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MikeRosoft.Data
{
    public static class SeedData
    {
        //public static void Initialize(UserManager<ApplicationUser> userManager,
        //            RoleManager<IdentityRole> roleManager)
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();


            //List<string> rolesNames = new List<string> { "Manager", "Customer" };

            //SeedRoles(roleManager, rolesNames);
            //SeedUsers(userManager, rolesNames);
            ////SeedBoats_Rentals(dbContext);
            SeedProducts(dbContext);
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

        public static void SeedUsers(UserManager<ApplicationUser> userManager, List<string> roles)
        {

        }


        public static void SeedProducts(ApplicationDbContext dbContext)
        {
            Product product_1 = new Product()
            {
                id = 1,
                title = "Memoria RAM",
                description = "8 GB",
                brand = "Kingston",
                precio = 130,
                stock = 3
            };
            dbContext.Products.Add(product_1);

            Product product_2 = new Product()
            {
                id = 2,
                title = "Memoria RAM",
                description = "16 GB",
                brand = "Kingston",
                precio = 130,
                stock = 0
            };
            dbContext.Products.Add(product_2);

            Product product_3 = new Product()
            {
                id = 3,
                title = "Memoria RAM",
                description = "325 GB",
                brand = "Kingston",
                precio = 130,
                stock = 2
            };
            dbContext.Products.Add(product_3);

            dbContext.SaveChanges();
        }
    }

}

