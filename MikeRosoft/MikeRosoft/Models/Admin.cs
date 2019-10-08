using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class Admin : ApplicationUser
    {
        public virtual int admin { get; set; }

        public virtual IList<Recommendation> Recommendations { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime contractStarting { set; get; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public virtual DateTime contractEnding { set; get; }

        public virtual IList<Ban> GetBans { get; set; }

        public override bool Equals(object obj)
        {
            Admin otherAdmin = (Admin) obj;
            bool result = this.Id.Equals(otherAdmin.Id) && (this.contractStarting.Equals(otherAdmin.contractStarting)) 
                && (this.contractEnding.Equals(otherAdmin.contractEnding)) && (this.GetBans.Count == otherAdmin.GetBans.Count);
            for(int i=0; i<this.GetBans.Count; i++)
            {
                result = result && this.GetBans.ElementAt(i).Equals(otherAdmin.GetBans.ElementAt(i)) ;
            }

            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
