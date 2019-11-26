using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class Brand
    {
        [Key]
        public virtual int Brandid { set; get; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public virtual string Name { set; get; }

        public virtual IList<Product> Products { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Brand brand &&
                   Brandid == brand.Brandid &&
                   Name == brand.Name &&
                   EqualityComparer<IList<Product>>.Default.Equals(Products, brand.Products);
        }
    }
}
