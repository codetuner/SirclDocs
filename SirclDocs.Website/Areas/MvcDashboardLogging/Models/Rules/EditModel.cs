using SirclDocs.Website.Data.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SirclDocs.Website.Areas.MvcDashboardLogging.Models.Rules
{
    public class EditModel : BaseEditModel<LogActionRule>
    {
        public bool IsNew => this.Item.Id == 0;
    }
}
