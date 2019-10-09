using System;
using System.Collections.Generic;
using System.Text;

namespace MikeRosoft.Design
{
    public class UserRequest
    {
        public virtual int requestID { get; set; }
        public virtual ReturnRequest ReturnRequest { get; set; }
        public virtual string userID { get; set; }
        public virtual User User { get; set; }
    }
}