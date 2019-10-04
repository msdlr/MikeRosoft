using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MikeRosoft.Models
{
    public class BanForUser
    {
        //Attributes
        [Key]
        public virtual int ID { get; set; }
        public virtual string AdditionalComment { get; set; }
        [Required(ErrorMessage = "Enter valid date")]
        public virtual DateTime Start { get; set; }
        [Required(ErrorMessage = "Enter valid date")]
        public virtual DateTime End { get; set; }

        //Relationships
        public int GetBanID { get; set; }
        [ForeignKey("GetBanID")]
        public virtual Ban GetBan { get; set; }

        public string GetUserId { get; set; }
        [ForeignKey("GetUserId")]
        public virtual ApplicationUser GetUser { get; set; }

        public string GetBanTypeName { get; set; }
        [ForeignKey("GetBanTypeName")]
        public virtual BanType GetBanType { get; set; }
    }
}