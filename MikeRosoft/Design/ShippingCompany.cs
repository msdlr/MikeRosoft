using System;
using System.Collections.Generic;
using System.Text;

namespace MikeRosoft.Design
{
    public class ShippingCompany
    {
        // attributes
        public virtual int ID { get; set; }
        public virtual IList<UserRequest> userRequests { get; set; }
        public virtual string name { get; set; }
    }
}
