using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MikeRosoft.Models
{
    public class BanType
    {
        //Attributes
        [Key]
        public virtual string TypeName { set; get; }
        public TimeSpan Duration { get; set; }

        //Relationships
        public virtual IList<BanForUser> GetBanForUsers { get; set; }

        //Equals
        public override bool Equals(object obj)
        {
            BanType otherType = (BanType)obj;
            bool result=this.TypeName.Equals(otherType.TypeName) && (this.Duration.Equals(otherType.Duration)) && (this.GetBanForUsers.Count == otherType.GetBanForUsers.Count);
            for (int i=0; i<this.GetBanForUsers.Count; i++)
            {
                result = result && this.GetBanForUsers.ElementAt(i).Equals(otherType.GetBanForUsers.ElementAt(i));
            }
            return result;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
