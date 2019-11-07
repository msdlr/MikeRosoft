using System;
using System.Collections.Generic;

namespace MikeRosoft.Design
{
    public class ReturnRequest
    {
        //Attributes
        public virtual int ID { get; set; }
        public virtual IList<Order> orders { get; set; }
        public virtual IList<UserRequest> userRequests { get; set; }
        public virtual string title { get; set; }
    }
}