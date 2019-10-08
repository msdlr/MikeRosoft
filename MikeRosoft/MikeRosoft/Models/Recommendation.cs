using System;
using System.ComponentModel.DataAnnotation;
using System.ComponentModel.DataAnnotation.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace MikeRosoft.Models {
    public class Recommendation
    {
        [Key]
        public virtual int IdRecommendation { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public virtual DateTime date { get; set; }
        
        //Description about selected products
        [Required]
        [StringLength(180, MinimumLength = 1, ErrorMessage = "Description cannot be longer than 180 characters")]
        public virtual String description { get; set;  }
        
        //Relacion con admin N-1
        [Required]
        public virtual Admin admin { get; set; }
        
        //Relacion con Usuarios N-N
        public virtual IList<UserRecommend> UserRecommendations { get; set; }

        //Relacion con Productos N-N
        public virtual IList<ProductRecommend> ProductRecommendations { get; set; }

        public Recommendation()
        {
        
        }
    }
}
