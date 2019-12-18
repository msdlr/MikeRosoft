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
        public virtual int id { set; get; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public virtual string title { set; get; }

        
        [StringLength(500, MinimumLength = 50, ErrorMessage = "Cannot be longer than 50 characters.")]
        public virtual string description { set; get; }

        [Required]
        [Range(0, float.MaxValue)]
        public virtual float precio { set; get; }

        
        [Required]
        [Range(0, int.MaxValue)]
        public virtual int stock { set; get; }

        [Required]
        [Range(0, 5, ErrorMessage = "Integer points between 0 and 5")]
        public virtual int rate { set; get; }

        [ForeignKey("Brandid")]
        public virtual Brand brand{ get; set; }

        
        public virtual IList<ProductOrder> ProductOrders { get; set; }

        //MakeRecommendation
        public virtual IList<ProductRecommend> ProductRecommendations { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Product product &&
                   id == product.id &&
                   title == product.title &&
                   description == product.description &&
                   precio == product.precio &&
                   stock == product.stock &&
                   rate == product.rate &&
                   (brand.Brandid == product.brand.Brandid && brand.Name == product.brand.Name);
        }
    }
}
