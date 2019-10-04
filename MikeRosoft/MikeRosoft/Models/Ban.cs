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
    }
}
