using SirclDocs.Website.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SirclDocs.Website.Areas.MvcDashboardContent.Models.SecuredPath
{
    public class EditModel : BaseEditModel<Data.Content.SecuredPath>
    {
        public bool IsNew => this.Item.Id == 0;

        public List<string> PathsList { get; internal set; } = [];
    }
}
