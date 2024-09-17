using SirclDocs.Website.Data.Logging;
using SirclDocs.Website.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable enable

namespace SirclDocs.Website.Areas.MvcDashboardLogging.Models.Rules
{
    public class IndexModel : BaseIndexModel<LogActionRule>
    {
        public string? ApplicationFilter { get; set; }

        public string? AspectFilter { get; set; }

        public LogAction? ActionFilter { get; set; }

        public bool ActiveFilter { get; set; }
    }
}
