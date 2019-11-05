using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class Admin : ApplicationUser
    {
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
            return obj is Admin admin &&
                   base.Equals(obj) &&
                   contractStarting == admin.contractStarting &&
                   contractEnding == admin.contractEnding && base.Equals((ApplicationUser)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), contractStarting, contractEnding);
        }

        public static bool operator ==(Admin left, Admin right)
        {
            return EqualityComparer<Admin>.Default.Equals(left, right);
        }

        public static bool operator !=(Admin left, Admin right)
        {
            return !(left == right);
        }
    }
}
