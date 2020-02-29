using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models.OrderViewModels
{
    public class OrderDetailsForBuyViewModel
    {
            public virtual Order Order { get; set; }
       
            public virtual Product Product
            {
                get;
                set;
            }
       
            public virtual string Name
            {
                get;
                set;
            }
            public virtual string FirstSurname
            {
                get;
                set;
            }

            public virtual float Price
            {
                get;
                set;
            }



        

    }
}
