using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class Product
    {
        [Key]
        public virtual int Id { set; get; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public virtual string Title { set; get; }

        
        [StringLength(500, MinimumLength = 50, ErrorMessage = "Cannot be longer than 50 characters.")]
        public virtual string Description { set; get; }

        [Required]
        [Range(0, float.MaxValue)]
        public virtual float Price { set; get; }

        
        [Required]
        [Range(0, int.MaxValue)]
        public virtual int Stock { set; get; }

        [Required]
        [Range(0, 5, ErrorMessage = "Integer points between 0 and 5")]
        public virtual int Rate { set; get; }

        [ForeignKey("Brandid")]
        public virtual Brand brand{ get; set; }

        public virtual IList<ProductOrder> ProductOrders { get; set; }

        //MakeRecommendation
        public virtual IList<ProductRecommend> ProductRecommendations { get; set; }

        public override bool Equals(object Other)
        {
            Product OtherPro = (Product)Other;
            bool result = (this.Id == OtherPro.Id) && (this.Title == OtherPro.Title)
                && (this.Description == OtherPro.Description) && (this.brand == OtherPro.brand) && (this.Stock == OtherPro.Stock) && (this.Price == OtherPro.Price) && (this.ProductRecommendations.Count == OtherPro.ProductRecommendations.Count) && (this.ProductOrders.Count == OtherPro.ProductOrders.Count);
            for (int i = 0; i < this.ProductRecommendations.Count; i++)
            {
                result = result && (this.ProductRecommendations.ElementAt(i).Equals(OtherPro.ProductRecommendations.ElementAt(i)));
            }
            for (int i = 0; i < this.ProductOrders.Count; i++)
            {
                result = result && (this.ProductRecommendations.ElementAt(i).Equals(OtherPro.ProductRecommendations.ElementAt(i)));
            }
            return result;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
