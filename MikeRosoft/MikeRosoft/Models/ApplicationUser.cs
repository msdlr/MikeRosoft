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
        [Key]
        //[Required]
        [StringLength(9)]
        override
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter name")]
        [StringLength(50, MinimumLength = 1)]
        public virtual string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter province")]
        [Display(Name = "First Surname")]
        public virtual string FirstSurname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter province")]
        [Display(Name = "Second Surname")]
        public virtual string SecondSurname { get; set; }

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
