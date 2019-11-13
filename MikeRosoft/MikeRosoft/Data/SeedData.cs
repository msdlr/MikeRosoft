using MikeRosoft.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using MikeRosoft.Data;

namespace MikeRosoft.Data
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
                Admin user = new Admin();
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
                User user = new User();
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
                User user = new User();
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
                User user = new User();
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
        public static void SeedProducts(ApplicationDbContext dbContext)
        {
            //Genres and movies are created so that they are available whenever the system is run
            Product product;
            Brand brand = dbContext.Brand.FirstOrDefault(m => m.Name.Contains("HP"));
            if (brand == null)
            {
                brand = new Brand()
                {
                    Name = "HP"
                };
                dbContext.Brand.Add(brand);
            }

            if (!dbContext.Products.Any(m => m.title.Contains("Gaming Mouse")))
            {
                product = new Product { title = "Gaming Mouse", description = "Upgrade your computer setup with this ENHANCE Voltaic wired gaming mouse. Three DPI settings let you customize the sensitivity to your liking for better precision, and six easy-to-reach programmable buttons makes common tasks quicker. This ENHANCE Voltaic wired gaming mouse has a plug-and-play design for easy compatibility and quick installation.", brand = brand, precio = 20, stock = 100, rate = 4 };
                dbContext.Products.Add(product);
            }

            brand = dbContext.Brand.FirstOrDefault(m => m.Name.Contains("Toshiba"));
            if (brand == null)
            {
                brand = new Brand()
                {
                    Name = "Toshiba"
                };
                dbContext.Brand.Add(brand);
            }
            if (!dbContext.Products.Any(m => m.title.Contains("Silent Mouse")))
            {
                product = new Product { title = "Silent Mouse", description = "Quickly input commands with this Logitech M510 910-001822 mouse that features laser technology for precision tracking on most surfaces. The Logitech unifying receiver allows simple wireless connectivity.Make navigation simple. The zoom function lets you magnify images (requires Logitech SetPoint software for Windows or Logitech Control Center software for Mac OS X; download required).", brand = brand, precio = 30, stock = 89, rate = 5 };

            dbContext.Products.Add(product);
            }
            brand = dbContext.Brand.FirstOrDefault(m => m.Name.Contains("Lenovo"));
            if (brand == null)
            {
                brand = new Brand()
                {
                    Name = "Lenovo"
                };
                dbContext.Brand.Add(brand);
            }
            if (!dbContext.Products.Any(m => m.title.Contains("Dark Keyboard")))
            {
                product = new Product { title = "Dark Keyboard", description = "Add color to your gaming rig with this Corsair Strafe mechanical gaming keyboard. It has 100 percent cherry MX RGB key-switches for up to 30 percent less noise during key presses and dedicated volume and multimedia controls, so you can quickly make adjustments. This Corsair Strafe mechanical gaming keyboard has a USB port for use with peripherals.", brand = brand, precio = 25, stock = 50, rate = 3 };
                dbContext.Products.Add(product);
            }
            dbContext.SaveChanges();
        }
    }
}


