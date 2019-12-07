using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models.BanViewModels
{
    public class BanDetailsViewModel
    {
        public Ban ban { get; set; }
        public Admin admin { get; set; }
        public IList<User> bannedUsers = new List<User>();
        public IList<string> BanTypeNames = new List<string>();
    }
}
