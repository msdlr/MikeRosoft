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
        public virtual string Street { get; set; }

        [System.ComponentModel.DataAnnotations.DataType(DataType.MultilineText)]
        [Display(Name = "City")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a city")]
        public string City { get; set; }

        [System.ComponentModel.DataAnnotations.DataType(DataType.MultilineText)]
        [Display(Name = "Province")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a province")]
        public string Province {get; set;}

        [System.ComponentModel.DataAnnotations.DataType(DataType.MultilineText)]
        [Display(Name = "Country")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a country")]
        public string Country { get; set;}






        //ATT NEEDED FOR UC_buyProduct

        public virtual IList<Order> orders { get; set; }






        //Atributos para relaciones

        public virtual IList<BanForUser> BanRecord { get; set; }

        public override bool Equals(object obj)
        {
            User otherUser = (User)obj;
            bool result = (this.Id.Equals(otherUser.Id)) && (this.Street.Equals(otherUser.Street)) && (this.City.Equals(otherUser.City))
                && (this.Province.Equals(otherUser.Province)) && (this.Country.Equals(otherUser.Country));

            for (int i = 0; i < this.BanRecord.Count; i++)
            {
                result = result && (this.BanRecord.ElementAt(i).Equals(BanRecord.ElementAt(i)));
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
