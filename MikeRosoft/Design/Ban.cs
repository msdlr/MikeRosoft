using System;
using System.Collections.Generic;

namespace MikeRosoft.Design
{
    public class Ban
    {
        //Attributes
        public virtual int ID { get; set; }
        public string GetAdminId { get; set; }
        public virtual Admin GetAdmin { get; set; }
        public virtual IList<BanForUser> GetBanForUsers { get; set; }
        public virtual DateTime BanTime { get; set; }
    }
}
