using MikeRosoft.Models;
using System.Collections.Generic;

namespace MikeRosoft.Design
{
    public class User : ApplicationUser
    {
        public virtual string Street { get; set; }
        public string City { get; set; }

        public string Province {get; set;}

        public string Country { get; set;}






        //ATT NEEDED FOR UC_buyProduct
        public virtual IList<Order> Orders { get; set; }





        //Atributos para relaciones

        public virtual IList<BanForUser> BanRecord { get; set; }
    }
}
