using System;
using System.ComponentModel.DataAnnotation;
using System.ComponentModel.DataAnnotation.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class UserRecommend
    {
        [Key]
        public virtual int ID { get; set; }

        public virtual Recommendation recommendation { get; set; }

        public virtual User user { get; set; }

        public UserRecommend()
        {
        }
    }
}
