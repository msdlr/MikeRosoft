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
        public virtual string title { get; set; }

        // relationships
        [Required]
        public virtual IList<UserRequest> userRequests { get; set; }
        [Required]
        public virtual IList<Order> orders { get; set; }
    }
}
