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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public virtual DateTime Start { get; set; }
        [Required(ErrorMessage = "Enter valid date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}")]
        public virtual DateTime End { get; set; }

        //Relationships
        [Required]
        public int GetBanID { get; set; }
        [ForeignKey("GetBanID")]
        public virtual Ban GetBan { get; set; }

        [Required]
        public string GetUserId { get; set; }
        [ForeignKey("GetUserId")]
        public virtual User GetUser { get; set; }

        [Required]
        public int GetBanTypeID { get; set; }
        [ForeignKey("GetBanTypeID")]
        public virtual BanType GetBanType { get; set; }

        //Equals
        public override bool Equals(object obj)
        {
            BanForUser bfu = (BanForUser)obj;
            bool result = (this.ID == bfu.ID) && (this.GetBanID == bfu.GetBanID) && (this.GetUserId == bfu.GetUserId) && (this.GetBanTypeID == bfu.GetBanTypeID)
                            && (this.AdditionalComment.Equals(bfu.AdditionalComment)) && this.Start.Equals(bfu.Start) && (this.End.Equals(bfu.End));

            return result;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}