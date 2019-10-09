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
        public virtual int ID { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product product { get; set; }

        [ForeignKey("RecommendationId")]
        public virtual Recommendation recommendation { get; set; }

        public ProductRecommend()
        {
        }
    }
}
