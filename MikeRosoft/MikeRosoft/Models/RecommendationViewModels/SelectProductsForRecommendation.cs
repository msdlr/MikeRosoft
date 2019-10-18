using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models.RecommendationViewModels
{
    public class SelectProductsForRecommendation
    {
        public IEnumerable<Product> products { get; set; }

        //filtro
        //public SelectList nombredelfiltro
        //...
        
        [Display(Name = "title")]
        public string productTitleSelected { get; set; }

        public SelectList brand;
        [Display (Name = "brand")]
        public string productBrandSelected { get; set; }

        [Display (Name = "precio")]
        public string productPrecioSelected { get; set; }

        //Atributos a mostrar cuando hacemos el filtro
        //[Display(name = "nombreatributo")]
        //public tipo productoatributoseleccionado {get; set;}
        //...
    }
}
