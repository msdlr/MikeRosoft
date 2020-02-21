using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class ProductRecommend
    {
        //[Key]
        //public virtual int ID { get; set; }

       // public virtual int ProductId { get; set; }
        //[ForeignKey("ProductId")]
        [Required]
        public virtual Product Product { get; set; }

        //public virtual int RecommendationId { get; set; }
        //[ForeignKey("RecommendationId")]
        [Required]
        public virtual Recommendation Recommendation { get; set; }

        public override bool Equals(object Other)
        {
            ProductRecommend ProdRec = (ProductRecommend)Other;
            bool result = (this.Product == ProdRec.Product) && (this.Recommendation == ProdRec.Recommendation);
            return result;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
