using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class ProductOrder
    {

        public virtual Product products { get; set; }


        public virtual Order orders { get; set; }

    }
}
