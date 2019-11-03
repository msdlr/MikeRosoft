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

        public override bool Equals(object Other)
        {
            Brand Otherbrand = (Brand)Other;
            bool result = (this.Brandid == Otherbrand.Brandid) && (this.Name == Otherbrand.Name) && (this.Products.Count == Otherbrand.Products.Count);
            for (int i = 0; i < this.Products.Count; i++)
            {
                result = result && (this.Products.ElementAt(i).Equals(Otherbrand.Products.ElementAt(i)));
            }
            return result;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
