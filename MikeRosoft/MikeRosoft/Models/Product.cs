using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models
{
    public class Product
    {
        [Key]
        public virtual int id { set; get; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public virtual string title { set; get; }

        
        [StringLength(500, MinimumLength = 50)]
        public virtual string description { set; get; }

        [Required]
        [Range(1, int.MaxValue)]
        public virtual float precio { set; get; }

        [Required]
        [Range(0, int.MaxValue)]
        public virtual int stock { set; get; }

    }
}
