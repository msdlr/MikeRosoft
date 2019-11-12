using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models.ProductViewModels
{
    public class CreateProductViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Cannot be longer than 50 characters.")]
        [Remote(action: "VerifyTitle", controller: "Products")]
        public virtual string Title { set; get; }

        [Required]
        [StringLength(500, MinimumLength = 50, ErrorMessage = "Cannot be smaller than 50 characters and longer than 500.")]
        [Display(Name = "Description")]
        public virtual string Description { set; get; }

        [Required]
        [Range(0, float.MaxValue, ErrorMessage = "Cannot be smaller than 1.")]
        [Display(Name = "Price")]
        public virtual float Price { set; get; }


        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Stock")]
        public virtual int Stock { set; get; }

        [Required]
        [Range(0, 5, ErrorMessage = "Integer points between 0 and 5")]
        [Display(Name = "Rate")]
        public virtual int Rate { set; get; }

        [Required]
        [Display(Name = "Brand Name")]
        public virtual String BrandName { get; set; }
    }
}
