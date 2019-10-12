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

        public virtual int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product product { get; set; }

        public virtual int RecommendationId { get; set; }
        [ForeignKey("RecommendationId")]
        public virtual Recommendation recommendation { get; set; }

        public virtual int RateId { get; set; }
        [ForeignKey("RateID")]
        public virtual Rate rate { get; set; }

        public override bool Equals(object Other)
        {
            ProductRecommend ProdRec = (ProductRecommend)Other;
            bool result = (this.ProductId == ProdRec.ProductId) && (this.RecommendationId == ProdRec.RecommendationId)
                && (this.RateId == ProdRec.RateId);
            return result;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
