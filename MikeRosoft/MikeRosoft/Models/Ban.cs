using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class Ban
    {
        //Attributes
        [Key]
        public virtual int ID { get; set; }

        //Relationships
        public string GetAdmin { get; set; }
        [ForeignKey("GetAdminDNI")]
        public virtual ApplicationUser GetAdminId { get; set; }

        public virtual IList<BanForUser> GetBanForUsers { get; set; }
        public virtual DateTime BanTime { get; set; }

        //Equals
        public override bool Equals(object Other)
        {
            Ban OtherBan = (Ban)Other;
            bool result = (this.ID == OtherBan.ID) && (this.GetAdminId == OtherBan.GetAdminId) 
                && (this.BanTime == OtherBan.BanTime) && (this.ID == OtherBan.ID) && (this.GetBanForUsers.Count == OtherBan.GetBanForUsers.Count);
            for (int i=0;i<this.GetBanForUsers.Count;i++)
            {
                result = result && (this.GetBanForUsers.ElementAt(i).Equals(OtherBan.GetBanForUsers.ElementAt(i)));
            }
            return result;
        }
    }
}
