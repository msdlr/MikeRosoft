using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models.RecommendationViewModels
{
    public class SelectProductsForRecommendationViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        //Used to filter by brand
        public SelectList Brands;
        [Display(Name = "Brand")]
        public string productBrandSelected { get; set; }
        //Used to filter by title of product
        [Display(Name ="Title")]
        public string productTitle { get; set; }
        //Used to filter by price of product
        [Display(Name = "Price")]
        [Range(1,int.MaxValue, ErrorMessage = "Introduce a price bigger than 0")]
        public int productPrice { get; set; }
        //Used to filter by rate of product
        [Display(Name = "Rate")]
        [Range(1,5,ErrorMessage ="Rate between 1 and 5")]
        public int productRate { get; set; }
    }
}
