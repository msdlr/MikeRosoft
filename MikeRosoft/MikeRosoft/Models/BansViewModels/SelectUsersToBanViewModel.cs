using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models.BansViewModels
{
    public class SelectUsersToBanViewModel
    {
        public IEnumerable<BanForUser> banForUsers { get; set; }

        [Display(Name = "Name")]
        public string NameSelected { get; set; }

        [Display(Name = "1st surname")]
        public string sur1Selected { get; set; }
        [Display(Name = "2nd surname")]
        public string sur2Selected { get; set; }
        [Display(Name = "ID")]
        public string IdSelected { get; set; }

    }
}
