using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MikeRosoft.Models
{
    public class UserRequest
    {
        public virtual int requestID { get; set; }
        [ForeignKey("requestID")]
        public virtual ReturnRequest ReturnRequest { get; set; }
        public virtual string userID { get; set; }
        [ForeignKey("userID")]
        public virtual User User { get; set; }
    }
}