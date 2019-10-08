using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MikeRosoft.Models
{
    public class ReturnRequest
    {
        // attributes
        [Key]
        public virtual int ID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public virtual string title       { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public virtual string description { get; set; }

        // relationships
        [Required]
        public virtual User user { get; set; }
        [Required]
        public virtual ShippingCompany shippingCompany { get; set; }
        [Required]
        public virtual IList<Order> orders { get; set; }
    }
}
