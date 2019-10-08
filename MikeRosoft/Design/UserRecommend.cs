using System;
using System.Collections.Generic;


namespace MikeRosoft.Design
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
