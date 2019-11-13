using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MikeRosoft.Models.OrderViewModels
{
    public class SelectedProductsForBuyViewModel
    {

        public string[] IdsToAdd { get; set; }

    }
}
