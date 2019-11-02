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
            builder.UseInMemoryDatabase("MikeRosoft")
                    .UseInternalServiceProvider(serviceProvider);
            return builder.Options;
        }

        /*public static void InitializeDbProductsForTests(ApplicationDbContext db)
        {
            db.Products.Add(new Models.Product { title = "Mouse" });
            db.Products.Add(new Models.Product { title = "Screen" });
            db.Products.Add(new Models.Product { title = "Keyboard" });
            db.SaveChanges();

        }*/

        //public static void ReInitializeDbGenresForTests(ApplicationDbContext db)
        //{
        //    db.Genre.RemoveRange(db.Genre);
        //    db.SaveChanges();
        //}




    }
}
