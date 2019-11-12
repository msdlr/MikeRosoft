using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MikeRosoft.Models.OrderViewModels
{
    public class SelectProductsForBuyViewModel
    {

        public IEnumerable<Product> Products { get; set; }

        [Display(Name = "Title")]
        public string titleSelected { get; set; }

        [Display(Name = "Brand")]
        public string brandSelected { get; set; }

    }
}
