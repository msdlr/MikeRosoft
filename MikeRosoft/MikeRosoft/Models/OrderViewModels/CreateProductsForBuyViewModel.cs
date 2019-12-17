using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models.OrderViewModels
{
    public class CreateProductsForViewModel
    {
        public virtual int id { get; set; }


        //
        public virtual IList<ProductOrder> ProductOrders { get; set; }


        //
        public string userId { get; set; }

        public string UserName { get; set; }

        public string FirstSurname { get; set; }

        public string SecondSurname { get; set; }


        //Momento en el que se realiza order
        public DateTime orderDate { get; set; }



        //Día en el que llegará order
        public DateTime arrivalDate { get; set; }



        //Precio total de order
        public virtual float totalprice { get; set; }


        //Address
        [DataType(DataType.MultilineText)]
        [Display(Name = "Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "You have to specify your address")]
        public virtual string address { get; set; }


        //PAYMENT METHOD
        public String PaymentMethod { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Card Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "You have to specify your card number")]
        [CreditCard]
        public string Card { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Card CVC")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "You have to specify your card CVC, you can find it on the back of your credit card")]
        public string cardCVC { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Card expiration date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "You have to specify your card expiration date")]
        public DateTime cardExpiration { get; set; }
    }
}