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
        [StringLength(9)]
        public virtual String DNI { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter name")]
        [StringLength(50, MinimumLength = 1)]
        public virtual string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter province")]
        [Display(Name = "First Surname")]
        public virtual string FirstSurname { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Enter province")]
        [Display(Name = "Second Surname")]
        public virtual string SecondSurname { get; set; }
    }
}
