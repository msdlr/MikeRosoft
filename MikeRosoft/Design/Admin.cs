using System;
using System.Collections.Generic;

namespace MikeRosoft.Design
{
    public class Admin : ApplicationUser
    {
        public virtual int IdAdmin {get; set;}

        public virtual IList<Recommendation> Recommendations { get; set; }

        public virtual DateTime contractStarting { set; get; }

        public virtual DateTime contractEnding { set; get; }

        public virtual IList<Ban> GetBans { get; set; }
    }
}
