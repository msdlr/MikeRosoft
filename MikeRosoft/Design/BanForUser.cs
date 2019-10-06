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
        
        public string GetUserId { get; set; }
        
        public string GetBanTypeName { get; set; }
    }
}