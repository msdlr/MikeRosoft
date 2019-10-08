using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class ProductRecommend
    {
        [Key]
        public virtual int ID { get; set; }

        public virtual Product product { get; set; }

        public virtual Recommendation recommendation { get; set; }

        public ProductRecommend()
        {
        }
    }
}
