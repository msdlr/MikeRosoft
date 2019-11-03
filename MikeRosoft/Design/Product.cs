using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Design
{
    public class Product
    {
        
        public virtual int Id { set; get; }

        public virtual string Title { set; get; }
        
        public virtual string Description { set; get; }

        public virtual Brand Brand { set; get; }

        public virtual float Precio { set; get; }
  
        public virtual int Stock { set; get; }

        public virtual int Rate { set; get; }

        public virtual IList<ProductOrder> ProductOrders { get; set; }

        public virtual IList<ProductRecommend> ProductRecommendations { get; set; }

    }
}
