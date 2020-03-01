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
        public string GetAdminId { get; set; }
        [ForeignKey("GetAdminDNI")]
        public virtual Admin GetAdmin { get; set; }

        public virtual IList<BanForUser> GetBanForUsers { get; set; }
        public virtual DateTime BanTime { get; set; }

        //Equals

        //public override bool Equals(object Other)
        //{
        //    if()
        //}

        public override bool Equals(object Other)
        {
            //If we substact DateTime and the result is negative an exception is thrown, so we can call this function with the arguments swiped to the result is positive
            try
            {   Ban OtherBan = (Ban)Other;
                //Substract the datetimes (exception expected)
                TimeSpan difference = this.BanTime - OtherBan.BanTime;
                TimeSpan threshold = new TimeSpan(0,1,0);

                bool result = (this.ID == OtherBan.ID) && (this.GetAdminId == OtherBan.GetAdminId) && (this.ID == OtherBan.ID) && (this.GetBanForUsers.Count == OtherBan.GetBanForUsers.Count) && difference < threshold;
            return result;
            }
            catch (System.OverflowException e)
            {
                return Other.Equals(this);
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
