using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MikeRosoft.Models
{
    public class UserRecommend
    {
        //[Key]
        public virtual int ID { get; set; }

        //[ForeignKey("RecommendationId")]
        public virtual Recommendation recommendation { get; set; }

        //[ForeignKey("UserId")]
        public virtual User user { get; set; }

        public UserRecommend()
        {
        }
    }
}
