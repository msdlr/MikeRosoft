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


        // UC Return Item
        public virtual IList<UserRequest> userRequests { get; set; }

        public virtual IList<Order> orders { get; set; }


        //UC_MakeRecommendation
        public virtual IList<UserRecommend> UserRecommendations { get; set; }



        //Atributos para relaciones

        public virtual IList<BanForUser> BanRecord { get; set; }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   base.Equals( (ApplicationUser) obj) &&
                   Street == user.Street &&
                   City == user.City &&
                   Province == user.Province &&
                   Country == user.Country;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(base.GetHashCode(), Street, City, Province, Country);
        }

        public static bool operator ==(User left, User right)
        {
            return EqualityComparer<User>.Default.Equals(left, right);
        }

        public static bool operator !=(User left, User right)
        {
            return !(left == right);
        }
    }
}
