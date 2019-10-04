using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class Ban
    {
        public virtual Admin GetAdmin { get; set; }
        public virtual IList<BanForUser> GetBanForUsers { get; set; }
        public virtual DateTime BanTime {get;set;}
    }
}
