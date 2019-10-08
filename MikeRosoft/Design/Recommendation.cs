﻿using System;
using System.Collections.Generic;

namespace MikeRosoft.Design
{
    public class Recommendation
    {
        public virtual int IdRecommendation { get; set; }

        public virtual DateTime date { get; set; }

        public virtual String description { get; set; }

        public virtual Admin admin { get; set; }

        public virtual IList<UserRecommend> UserRecommendations { get; set; }

        public virtual IList<ProductRecommend> ProductRecommendations { get; set; }

        public Recommendation()
        {

        }
    }
}