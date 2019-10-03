using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MikeRosoft.Models
{
    public class ReturnRequest
    {
        /* keys, foreign keys */
        [Key] 
        public virtual int ID         { get; set; }
        [ForeignKey("Customer")] [Required]
        public virtual int userID     { get; set; }
        [ForeignKey("Order")] [Required]
        public virtual int orderID    { get; set; }
        [ForeignKey("ShippingCompany")] [Required]
        public virtual int shipCompID { get; set; }

        /* other attributes */
        [Required]
        public virtual string title       { get; set; }
        public virtual string description { get; set; }
    }
}
