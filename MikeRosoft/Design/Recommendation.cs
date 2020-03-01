using System;
using System.Collections.Generic;

namespace MikeRosoft.Design
{
    public class Recommendation
    {
        public virtual int IdRecommendation { get; set; }

        public virtual string NameRec { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual string Description { get; set; }

        public virtual Admin Admin { get; set; }

       // public virtual IList<UserRecommend> UserRecommendations { get; set; }

        public virtual IList<ProductRecommend> ProductRecommendations { get; set; }

        public Recommendation()
        {

        }
    }
}
