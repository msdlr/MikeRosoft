using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Design
{
    public class ProductRecommend
    {
        //[Key]
        public virtual int ID { get; set; }

        public virtual Product Product { get; set; }

        public virtual Recommendation Recommendation { get; set; }

        public ProductRecommend()
        {
        }
    }
}
