using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class UserRecommend
    {
        //[Key]
        //public virtual int ID { get; set; }

        public virtual int RecommendationId { get; set; }
        [ForeignKey("RecommendationId")]
        public virtual Recommendation recommendation { get; set; }

        public virtual int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User user { get; set; }

        public UserRecommend()
        {
        }
    }
}
