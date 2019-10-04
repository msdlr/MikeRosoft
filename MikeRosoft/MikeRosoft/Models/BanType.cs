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
        public int BanForUserID { get; set; }
        [ForeignKey("BanForUserID")]
        public virtual BanForUser GetBanForUser { get; set; }

    }
}
