using System.Linq;
using System.Threading.Tasks;

public class ProductRecommend
{
    [Key]
    public virtual int ID { get; set; }

    public virtual Product product { get; set; }

    public virtual Recommendation recommendation { get; set; }

    public ProductRecommend()
	{
	}
}
