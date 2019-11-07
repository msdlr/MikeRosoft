using System;
using System.Collections.Generic;
using System.Text;

namespace MikeRosoft.Design
{
    public class UserRequest
    {
        public virtual int requestID { get; set; }
        public virtual ReturnRequest returnRequest { get; set; }
        public virtual string userID { get; set; }
        public virtual User user { get; set; }

        public virtual ShippingCompany shippingCompany { get; set; }
        public virtual string summary { get; set; }
        public virtual DateTime date { get; set; }
        public virtual DateTime expDate { get; set; }
    }
}