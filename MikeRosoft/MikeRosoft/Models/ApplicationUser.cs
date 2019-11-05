using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter name")]
        [StringLength(50, MinimumLength = 1)]
        public virtual string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter first surname")]
        [Display(Name = "First Surname")]
        public virtual string FirstSurname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter second surname")]
        [Display(Name = "Second Surname")]
        public virtual string SecondSurname { get; set; }

        [System.ComponentModel.DataAnnotations.DataType(DataType.MultilineText)]
        [Display(Name = "DNI")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "No DNI")]
        [RegularExpression(@"(\d{8})([-]?)([A-Z]{1})")]
        public string DNI { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ApplicationUser user && base.Equals( (IdentityUser) obj ) &&
                   Name == user.Name &&
                   FirstSurname == user.FirstSurname &&
                   SecondSurname == user.SecondSurname &&
                   DNI == user.DNI;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, FirstSurname, SecondSurname, DNI);
        }
    }
}
