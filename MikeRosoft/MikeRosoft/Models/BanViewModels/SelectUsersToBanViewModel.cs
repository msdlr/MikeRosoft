using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models.BanViewModels
{
    public class SelectUsersToBanViewModel
    {
        //List of users
        public IEnumerable<User> Users { get; set; }
        
        //Filtering 
        [Display(Name = "ID")]
        public string userID { get; set; }
        [Display(Name = "Name")]
        public string userName { get; set; }
        [Display(Name = "1st surname")]
        public string userSurname1 { get; set; }
        [Display(Name = "2nd surname")]
        public string userSurname2 { get; set; }        
    }
}
