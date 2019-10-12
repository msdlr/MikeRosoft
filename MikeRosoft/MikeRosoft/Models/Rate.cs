using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MikeRosoft.Models
{
    public class Rate
    {
        [Key]
        public virtual int idRate { get; set; }

        [Range(0,5, ErrorMessage = "Integer points between 0 and 5")]
        public virtual int points { get; set; }

        [StringLength(120, MinimumLength = 1, ErrorMessage = "Description can not be empty or longer than 120 characters")]
        public virtual String description { get; set; }

        public virtual IList<ProductRecommend> ProductRecommendations { get; set; }

        public override bool Equals(object Other)
        {
            Rate OtherRate = (Rate)Other;
            bool result = (this.idRate == OtherRate.idRate) && (this.points == OtherRate.points)
                && (this.description == OtherRate.description) && (this.ProductRecommendations.Count == OtherRate.ProductRecommendations.Count);
            for (int i = 0; i < this.ProductRecommendations.Count; i++)
            {
                result = result && (this.ProductRecommendations.ElementAt(i).Equals(OtherRate.ProductRecommendations.ElementAt(i)));
            }
            return result;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
