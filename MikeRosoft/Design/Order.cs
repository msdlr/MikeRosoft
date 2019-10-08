using MikeRosoft.Design;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class Order
    {

        //
        public virtual int id { get; set; }


        //
        public virtual IList<ProductOrder> ProductOrders { get; set; }


        //
        public string userId { get; set; }
        public virtual User user { get; set; }


        //Momento en el que se realiza order
        public DateTime orderDate { get; set; }



        //Día en el que llegará order
        public DateTime arrivalDate { get; set; }


        //Precio total de order
        public virtual float totalprice { get; set; }




        //PAYMENT METHOD
       public String PaymentMethod{ get; set; }

        [CreditCard]
        public string Card { get; set; }

        public string cardCVC { get; set; }

        public DateTime cardExpiration { get; set; }



    }
}
