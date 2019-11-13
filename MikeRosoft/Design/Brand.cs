using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Design
{
    public class Brand
    {
        public virtual int Brandid { set; get; }

        public virtual string Name { set; get; }

        public virtual IList<Product> Products { get; set; }

    }
}
