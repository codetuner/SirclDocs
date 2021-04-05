using SirclDocs.Website.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SirclDocs.Website.Areas.MvcDashboardContent.Models.SecuredPath
{
    public class EditModel : BaseEditModel<Data.Content.SecuredPath>
    {
        public List<string> PathsList { get; internal set; }
    }
}
