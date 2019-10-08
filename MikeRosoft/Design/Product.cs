using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Design
{
    public class Product
    {
        
        public virtual int id { set; get; }

        
        public virtual string title { set; get; }

        
        
        public virtual string description { set; get; }


        public virtual string brand { set; get; }


        public virtual float precio { set; get; }

        
        
        public virtual int stock { set; get; }


        public virtual IList<ProductOrder> productOrders { get; set; }

        public virtual IList<ProductRecommend> ProductRecommendations { get; set; }

    }
}
