using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class ProductOrder
    {

        public int productId { get; set; }
        [ForeignKey("productId")]
        public virtual Product products { get; set; }


        public int orderId { get; set; }
        [ForeignKey("orderId")]
        public virtual Order orders { get; set; }

        [Required]
        [DefaultValue(1)]
        public virtual int quantity { get; set; }


    }
}
