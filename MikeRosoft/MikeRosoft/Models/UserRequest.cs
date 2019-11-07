using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MikeRosoft.Models
{
    public class UserRequest
    {
        // keys
        public virtual int requestID { get; set; }
        [ForeignKey("requestID")]
        public virtual ReturnRequest returnRequest { get; set; }

        public virtual string userID { get; set; }
        [ForeignKey("userID")]
        public virtual User user { get; set; }

        // other attributes
        [Required]
        public virtual ShippingCompany shippingCompany { get; set; }
        [Required]
        public virtual string summary { get; set; }
        [Required]
        public virtual DateTime date { get; set; }
        public virtual DateTime expDate { get; set; }
    }
}