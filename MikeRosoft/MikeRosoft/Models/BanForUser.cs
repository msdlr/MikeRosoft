using System;

namespace MikeRosoft.Models
{
    public class BanForUser
    {
        //Attributes
        public virtual string AdditionalComment { get; set; }
        public virtual DateTime Start { get; set; } 
        public virtual DateTime End { get; set; } 

        //Relationships
        public virtual Ban GetBan { get; set; }

        public virtual User GetUser { get; set; }
        public virtual BanType GetBanType { get; set; }
    }
}