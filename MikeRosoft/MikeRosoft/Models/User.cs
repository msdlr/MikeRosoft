using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class User : ApplicationUser
    {
        [Display(Name = "Street")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a street")]
        public virtual  String Street { get; set; }

        [System.ComponentModel.DataAnnotations.DataType(DataType.MultilineText)]
        [Display(Name = "City")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a city")]
        public String City { get; set; }

        [System.ComponentModel.DataAnnotations.DataType(DataType.MultilineText)]
        [Display(Name = "Province")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a province")]
        public String Province {get; set;}

        [System.ComponentModel.DataAnnotations.DataType(DataType.MultilineText)]
        [Display(Name = "Country")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a country")]
        public String Country { get; set;}


        //Atributos para relaciones

        public virtual IList<BanForUser> BanRecord { get; set; }

    }
}
