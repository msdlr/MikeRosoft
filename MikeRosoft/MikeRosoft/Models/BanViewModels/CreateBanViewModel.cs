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
        //public List<string> IdsToAdd = new List<string>();
        public List<string> infoAboutUser = new List<string>();

        //Admin that bans the user
        public string adminId { get;set; }

        //List of BanForUser
        public virtual IList<BanForUser> BansForUsers { get; set; }

        //Ban type
        public SelectList BanTypesAvailable { get; set; }
        public IList<string> banTypeName { get; set; }
        public List<TimeSpan> defaultDuration { get; set; }

        //End and start date are inside the BanForUser IList

        //Comment for each BanForUser
        public List<string> AdditionalComment { set; get; }
    }
}
