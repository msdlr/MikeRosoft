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
        [Key]
        public virtual int id { get; set; }

        public virtual int ProductId { get; set; }
        [ForeignKey("id")]
        //[Required]
        public virtual Product Product { get; set; }

        public virtual int RecommendationId { get; set; }
        [ForeignKey("RecommendationId")]
        //[Required]
        public virtual Recommendation Recommendation { get; set; }

        public override bool Equals(object Other)
        {
            ProductRecommend ProdRec = (ProductRecommend)Other;
            bool result = (this.Product.id == ProdRec.Product.id) && (this.Recommendation.IdRecommendation == ProdRec.Recommendation.IdRecommendation);
            return result;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
