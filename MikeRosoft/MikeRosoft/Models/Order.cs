using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class Order
    {

        public virtual int id { get; set; }

        public virtual IList<ProductOrder> ProductOrders { get; set; }

        public virtual User user { get; set; }



    }
}
