using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class ShippingCompany
    {
        [Key]
        public virtual int ID { get; set; }
        public virtual IList<ReturnRequest> ReturnRequests { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public virtual string name { get; set; }
    }
}
