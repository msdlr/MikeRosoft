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

        //Equals
        public override bool Equals(object obj)
        {
            BanForUser bfu = (BanForUser)obj;
            bool result = (this.ID == bfu.ID) && (this.GetBanID == bfu.GetBanID) && (this.GetUserId == bfu.GetUserId) && (this.GetBanTypeName == bfu.GetBanTypeName)
                            && (this.AdditionalComment.Equals(bfu.AdditionalComment)) && this.Start.Equals(bfu.Start) && (this.End.Equals(bfu.End));

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}