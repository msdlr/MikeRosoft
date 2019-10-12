using System;
using System.Collections.Generic;

namespace MikeRosoft.Design
{
    public class Rate
    {
        public virtual int idRate { get; set; }

        public virtual int points { get; set; }

        public virtual String description { get; set; }

        public virtual IList<ProductRecommend> ProductRecommendations { get; set; }
    }
}
