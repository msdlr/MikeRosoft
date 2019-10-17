using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MikeRosoft.Models.BanViewModels
{
    public class CreateBanViewModel
    {
        public string[] IdsToAdd { get; set; }
        public virtual IEnumerable<BanForUser> BansForThis { get; set; }
    }
}
