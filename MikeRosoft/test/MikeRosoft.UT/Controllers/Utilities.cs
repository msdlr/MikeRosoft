using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MikeRosoft.Data;
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
