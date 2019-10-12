using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MikeRosoft.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MikeRosoft.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        //Creo que van aquí las listas

        public virtual DbSet<User> UserList { get; set; }
        public virtual DbSet<Admin> AdminList { get; set; }
        public virtual DbSet<Ban> BanList { get; set; }
        public virtual DbSet<BanForUser> BanForUserList { get; set; }
        public virtual DbSet<BanType> BanTypeList { get; set; }

        //MakeRecommendation
        public virtual DbSet<Recommendation> Recommendations { get; set;}
        public virtual DbSet<ProductRecommend> ProductRecommendations { get; set;}
        //public virtual DbSet<UserRecommend> UserRecommendations { get; set;}
        public virtual DbSet<Product> Products { get; set;}
        public virtual DbSet<Rate> Rates { get; set; }

        //ReturnItem
        public virtual DbSet<ReturnRequest> ReturnRequests { get; set; }
        public virtual DbSet<ShippingCompany> ShippingCompanies { get; set; }

        //Claves primarias para las relaciones n-n

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.Entity<ProductOrder>()
            .HasKey(pi => new { pi.orderId, pi.productId });
            builder.Entity<BanForUser>()
            .HasKey(pi => new { pi.GetBanID, pi.GetUserId });
            builder.Entity<ProductRecommend>()
            .HasKey(pi => new { pi.ProductId, pi.RecommendationId });
            /*builder.Entity<UserRecommend>()
            .HasKey(pi => new { pi.UserId, pi.RecommendationId });*/
        }
    }
}
