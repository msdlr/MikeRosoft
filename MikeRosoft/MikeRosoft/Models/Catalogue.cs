using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class Catalogue
    {
        [Key]
        public int idCatalogue;

        public virtual IList<Product> products { get; set; }

    }
}
