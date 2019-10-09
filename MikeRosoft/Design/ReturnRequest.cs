using System;
using System.Collections.Generic;

namespace MikeRosoft.Design
{
    public class ReturnRequest
    {
        //Attributes
        public virtual int ID { get; set; }
        public virtual IList<Order> orders { get; set; }
        public virtual IList<UserRequest> UserRequests { get; set; }
        public virtual ShippingCompany shippingCompany { get; set; }
        public virtual string title { get; set; }
        public virtual string description { get; set; }
    }
}