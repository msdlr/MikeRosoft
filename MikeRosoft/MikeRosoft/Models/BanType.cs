using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MikeRosoft.Models
{
    public class BanType
    {
        [Key]
        public virtual String TypeName {set;get;}
        public TimeSpan Duration { get; set; }
        public virtual BanForUser GetBanForUser { get; set; }

    }
}
