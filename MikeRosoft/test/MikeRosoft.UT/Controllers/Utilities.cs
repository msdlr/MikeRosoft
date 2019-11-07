using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MikeRosoft.Data;
using MikeRosoft.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MikeRosoft.UT.Controllers
{
    public static class Utilities
    {
        public static DbContextOptions<ApplicationDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase("AppForMovies")
                    .UseInternalServiceProvider(serviceProvider);
            return builder.Options;
        }

        public static void InitializeBanTypes(ApplicationDbContext db)
        {
            db.BanTypes.Add(new Models.BanType { TypeID = 1, TypeName = "Inappropiate behaviour", DefaultDuration = TimeSpan.FromHours(5)} );
            db.BanTypes.Add(new Models.BanType { TypeID = 2, TypeName = "Unpaid orders", DefaultDuration = TimeSpan.FromDays(7) });
            db.BanTypes.Add(new Models.BanType { TypeID = 3, TypeName = "Fraudulent information", DefaultDuration = TimeSpan.FromDays(99) });
        }

        public static void InitializeUsersToBan(ApplicationDbContext db)
        {
            User banned1 = new User();
            banned1.UserName = "elena@uclm.com";
            banned1.Email = "elena@uclm.com";
            banned1.Name = "Elena";
            banned1.FirstSurname = "Navarro";
            banned1.SecondSurname = "Martínez";
            banned1.DNI = "48484848B";
            db.Users.Add(banned1);

            User banned2 = new User();
            banned1.UserName = "test@uclm.com";
            banned1.Email = "test@uclm.com";
            banned1.Name = "Antonio";
            banned1.FirstSurname = "Martinez";
            banned1.SecondSurname = "Jimenez";
            banned1.DNI = "84172168P";
            db.Users.Add(banned2);
        }

        //public static void InitializeDbGenresForTests(ApplicationDbContext db)
        //{
        //    db.Genre.Add(new Genre { Name = "Comedy" });
        //    db.Genre.Add(new Genre { Name = "Drama" });
        //    db.Genre.Add(new Genre { Name = "Sitcom" });
        //    db.SaveChanges();

        //}

        //public static void ReInitializeDbGenresForTests(ApplicationDbContext db)
        //{
        //    db.Genre.RemoveRange(db.Genre);
        //    db.SaveChanges();
        //}




    }
}
