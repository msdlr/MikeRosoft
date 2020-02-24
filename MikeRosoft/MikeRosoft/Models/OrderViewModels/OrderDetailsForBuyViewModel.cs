using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models.OrderViewModels
{
    public class OrderDetailsForBuyViewModel
    {

        public class DetailsOrderViewModel
        {
            public virtual Product Product
            {
                get;
                set;
            }
            

        }

        public class RentalsMovie
        {
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

            [DataType(DataType.Date)]
            public virtual DateTime PickUpDate
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
}
