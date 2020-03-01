using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models.RecommendationViewModels
{
    public class RecommendationCreateViewModel
    {
        public virtual string Name { get; set; }

        [Display(Name = "First Surname")]
        public virtual string FirstSurname { get; set; }

        [Display(Name = "Second Surname")]
        public virtual string SecondSurname { get; set; }

        [Display(Name = "DNI")]
        public virtual string DNI { get; set; }

        public virtual string AdminId { get; set; }

        public virtual string NameRec { get; set; }

        public virtual DateTime Date { get; set; }

        //Description about selected products
        public virtual string Description { get; set; }

        //Relacion con Usuarios N-N
        //public virtual IList<UserRecommend> UserRecommendations { get; set; }

        //Relacion con Productos N-N
        public virtual IList<ProductRecommend> ProductRecommendations { get; set; }

        public RecommendationCreateViewModel()
        {
            ProductRecommendations = new List<ProductRecommend>();
        }
        public override bool Equals(object Other)
        {
            RecommendationCreateViewModel recommendation = Other as RecommendationCreateViewModel;
            int i;
            bool result = false;

            result = ((this.Name == recommendation.Name) && (this.FirstSurname == recommendation.FirstSurname) && (this.SecondSurname == recommendation.SecondSurname) && (this.DNI == recommendation.DNI) && (this.AdminId == recommendation.AdminId) && (this.Date == recommendation.Date) && (this.NameRec == recommendation.NameRec) && (this.Description == recommendation.Description) && (this.Date.Subtract(recommendation.Date) < new TimeSpan(0, 1, 0)));
            result = result && (this.ProductRecommendations.Count == recommendation.ProductRecommendations.Count);
            for(i = 0; i < this.ProductRecommendations.Count; i++)
            {
                result = result && (this.ProductRecommendations[i].Equals(recommendation.ProductRecommendations[i]));
            }
            return result;
        }
        /*public override int GetHashCode()
        {
            return base.GetHashCode();
        }*/
    }
}
