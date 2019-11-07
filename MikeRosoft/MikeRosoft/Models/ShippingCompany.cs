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
        [Required]
        public virtual IList<ReturnRequest> userRequests { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public virtual string name { get; set; }
    }
}
