using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Design
{
    public class ProductOrder
    {

        public int productId { get; set; }
        public virtual Product products { get; set; }


        public int orderId { get; set; }
        public virtual Order orders { get; set; }

        public virtual int quantity { get; set; }


    }
}
