using System;

namespace MikeRosoft.Design
{
    public class BanForUser
    {
        //Attributes
        public virtual int ID { get; set; }
        public virtual string AdditionalComment { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime End { get; set; }

        //Relationships
        public int GetBanID { get; set; }
        public virtual Ban GetBan { get; set; }
        public string GetUserId { get; set; }
        public virtual User GetUser { get; set; }
        public string GetBanTypeName { get; set; }
        public virtual BanType GetBanType { get; set; }
    }
}