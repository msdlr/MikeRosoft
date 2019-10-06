using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MikeRosoft.Design
{
    public class BanType
    {
        public virtual string TypeName { set; get; }
        public TimeSpan Duration { get; set; }

        //Relationships
        public virtual IList<BanForUser> GetBanForUsers { get; set; }
    }
}
