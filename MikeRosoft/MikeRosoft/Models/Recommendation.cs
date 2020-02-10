using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace MikeRosoft.Models
{
    public class Recommendation
    {
        [Key]
        public virtual int IdRecommendation { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name can not be empty or longer than 50 characters")]
        public virtual String name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public virtual DateTime date { get; set; }

        //Description about selected products
        [Required]
        [StringLength(180, MinimumLength = 1, ErrorMessage = "Description can not be empty or longer than 180 characters")]
        public virtual String description { get; set; }

        //Relacion con admin N-1
        /*[ForeignKey("AdminID")]
        public virtual Admin admin { get; set; }

        public virtual string AdminId { get; set;}*/
        [Required]
        public virtual Admin admin { get; set; }
        
        //Relacion con Usuarios N-N
        //public virtual IList<UserRecommend> UserRecommendations { get; set; }

        //Relacion con Productos N-N
        public virtual IList<ProductRecommend> ProductRecommendations { get; set; }

        public Recommendation()
        {
            ProductRecommendations = new List<ProductRecommend>();
        }

        public override bool Equals(object Other)
        {
            Recommendation OtherRec = (Recommendation)Other;
            bool result = (this.IdRecommendation == OtherRec.IdRecommendation) && (this.admin == OtherRec.admin)
                && (this.name == OtherRec.name) && (this.date == OtherRec.date) && (this.description == OtherRec.description) && (this.ProductRecommendations.Count == OtherRec.ProductRecommendations.Count);
            for (int i = 0; i < this.ProductRecommendations.Count; i++)
            {
                result = result && (this.ProductRecommendations.ElementAt(i).Equals(OtherRec.ProductRecommendations.ElementAt(i)));
            }
            return result;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
