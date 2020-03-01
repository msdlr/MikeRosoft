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
            builder.UseInMemoryDatabase("MikeRosoft")
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
            banned1.Id = "7ba98196-c2bf-4d6e-9d87-bdca85e81a0a";
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

        //Brands
        public static void InitializeDbBrandsForTests(ApplicationDbContext db)
        {
            db.Brand.Add(new Brand { Name = "Toshiba" });
            db.Brand.Add(new Brand { Name = "Microsoft" });
            db.Brand.Add(new Brand { Name = "Lenovo" });
            db.SaveChanges();
        }

        public static void ReInitializeDbBrandsForTests(ApplicationDbContext db)
        {
            db.Brand.RemoveRange(db.Brand);
            db.SaveChanges();
        }

        //Recommendations
        public static void InitializeDbProductsForTest(ApplicationDbContext db)
        {

            db.Products.AddRange(Utilities.Products); 
            db.SaveChanges();
            db.Admins.Add(new Admin { UserName = "peter@uclm.com", PhoneNumber = "967959595", Email = "peter@uclm.com", Name = "Peter", FirstSurname = "Jackson", SecondSurname = "García", DNI = "12345678D" });
            db.SaveChanges();
        }

        public static IList<Product> Products
        {
            get
            {
                IList<Product> products = new List<Product>();
                var brand = new Brand { Name = "HP" };
                //var brand2 = new Brand { Name = "Asus" };
                
                products.Add(new Product { id = 1, title = "Gamer Mouse", description = "1Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 20, stock = 100, rate = 4 });
                products.Add(new Product { id = 2, title = "Dark Keyboard", description = "2Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 25, stock = 50, rate = 3 });
                products.Add(new Product { id = 3, title = "Silence Mouse", description = "3Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description Description", brand = brand, precio = 30, stock = 89, rate = 5 });
                //products.Add(new Product { id = 4, title = "Monitor", description = "4Description", brand = brand, precio = 80, stock = 20, rate = 4 });
                return products;
            }
        }

        public static Recommendation Recommendation
        {
            get
            {
               // int i = 0;
                Recommendation recommendation = new Recommendation();
                Admin admin = new Admin { UserName = "peter@uclm.com", PhoneNumber = "967959595", Email = "peter@uclm.com", Name = "Peter", FirstSurname = "Jackson", SecondSurname = "García", DNI = "12345678D" };
                recommendation = new Recommendation { IdRecommendation = 0, Admin = admin, Description = "Recommendation Description", NameRec = "Recommendation1", ProductRecommendations = { }, Date = DateTime.Now};
                foreach (Product prod in Utilities.Products)
                {
                    recommendation.ProductRecommendations.Add(new ProductRecommend { Product = prod, ProductId = prod.id, Recommendation = recommendation, RecommendationId = recommendation.IdRecommendation });
                }
                return recommendation;
            }
        }

        public static void InitializeDbRecommendationForTests(ApplicationDbContext db)
        {
            db.Recommendations.Add(Utilities.Recommendation);
            db.SaveChanges();
            db.Admins.Add(new Admin { UserName = "peter@uclm.com", PhoneNumber = "967959595", Email = "peter@uclm.com", Name = "Peter", FirstSurname = "Jackson", SecondSurname = "García", DNI = "12345678D" });
            db.SaveChanges();
        }

        public static void ReInitializeDbRecommendationForTests(ApplicationDbContext db)
        {
            db.ProductRecommendations.RemoveRange(db.ProductRecommendations);
            db.Recommendations.RemoveRange(db.Recommendations);
            //db.Products.RemoveRange(db.Products);
            //db.Brand.RemoveRange(db.Brand);
            db.SaveChanges();
        }

        public static void ReInitializeDbProductsForTests(ApplicationDbContext db)
        {
            db.Brand.RemoveRange(db.Brand);
            db.Products.RemoveRange(db.Products);
            db.SaveChanges();
        }

        public static void InitializeDbAdminForTests(ApplicationDbContext db)
        {
            db.Admins.Add(new Admin {  UserName = "peter@uclm.com", PhoneNumber = "967959595", Email = "peter@uclm.com", Name = "Peter", FirstSurname = "Jackson", SecondSurname = "García",  DNI = "12345678D" });
            db.SaveChanges();
        }

        public static void ReInitializeDbAdminForTest(ApplicationDbContext db)
        {
            db.Admins.RemoveRange(db.Admins);
            db.SaveChanges();
        }

    }
}
