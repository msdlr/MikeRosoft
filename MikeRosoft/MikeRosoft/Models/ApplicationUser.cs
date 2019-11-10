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
            ApplicationUser OtherUser = (ApplicationUser) obj;
            return (this.Id.Equals(OtherUser.Id) && this.Name.Equals(OtherUser.Name) && this.FirstSurname.Equals(OtherUser.SecondSurname));
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
