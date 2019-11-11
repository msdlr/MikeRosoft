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

        // returnRequest moved to ProductOrder

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
        public DateTime arrivalDate { get; set; }


        //Precio total de order
        public virtual float totalprice { get; set; }


        //Dirección a la que se debe enviar
        public virtual String shippingAddress { get; set; }



        //PAYMENT METHOD
        [Display(Name = "Payment Method")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please, select your payment method for delivery")]
        public String PaymentMethod{ get; set; }

        [CreditCard]
        public string Card { get; set; }

        public string cardCVC { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime cardExpiration { get; set; }



    }
}
