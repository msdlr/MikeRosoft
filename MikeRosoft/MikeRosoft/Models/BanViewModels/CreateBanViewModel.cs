using Microsoft.AspNetCore.Mvc.Rendering;
using MikeRosoft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models.BanViewModels
{
    public class CreateBanViewModel
    {
        //IDs of users to ban
        public string[] UserIds { get; set; }

        public string[] infoAboutUser { get; set; }

        //List of BanForUser
        public IList<BanForUser> BansForUsers { get; set; }

        //Ban type
        public SelectList BanTypesAvailable { get; set; }
        public string[] banTypeName { get; set; }
        public int[] GetBanTypeID { get; set; }
        public TimeSpan[] defaultDuration { get; set; }

        //End and start date for each BanForUser
        public DateTime[] StartDate { get; set; }
        public DateTime[] EndDate { get; set; }

        //Comment for each BanForUser
        public string[] AdditionalComment { set; get; }
    }
}
