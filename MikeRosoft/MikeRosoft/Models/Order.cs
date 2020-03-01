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

        public virtual ReturnRequest ReturnRequest { get; set; }

        //
        public string userId { get; set; }
        [ForeignKey("userId")]
        public virtual User user { get; set; }


        //Momento en el que se realiza order
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime orderDate { get; set; }



        //Día en el que llegará order
        [DataType(DataType.MultilineText)]
        [Display(Name = "Delivery Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, set your address for delivery")]
        public string deliveryAddress { get; set; }

        public DateTime arrivalDate { get; set; }


        //Precio total de order
        public virtual float totalprice { get; set; }




        //PAYMENT METHOD

        [CreditCard]
        public string Card { get; set; }

        public string cardCVC { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime cardExpiration { get; set; }


        public override bool Equals(object Other)
        {

            try
            {
                Order otherOrder = (Order)Other;

                //TimeSpan difference = this.cardExpiration - otherOrder.cardExpiration;
                //TimeSpan threshold = new TimeSpan(0, 1, 0);


                if (id == otherOrder.id &&
                   ProductOrders.Count == otherOrder.ProductOrders.Count &&
                   userId == otherOrder.userId &&
                   Card == otherOrder.Card &&
                   cardCVC == otherOrder.cardCVC &&
                   cardExpiration.ToShortDateString() == otherOrder.cardExpiration.ToShortDateString()) return true;
                else return false;

                /*
                return Other is Order order &&
                   id == order.id &&
                   ProductOrders.Count == order.ProductOrders.Count &&
                   userId == order.userId &&
                   Card == order.Card &&
                   cardCVC == order.cardCVC &&
                   difference < threshold;
                   */
            }

            catch (System.OverflowException e)
            {
                return Other.Equals(this);
            }


        }

    }
}
