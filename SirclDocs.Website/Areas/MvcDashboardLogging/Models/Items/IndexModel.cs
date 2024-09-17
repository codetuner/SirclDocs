using SirclDocs.Website.Data.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable enable

namespace SirclDocs.Website.Areas.MvcDashboardLogging.Models.Items
{
    public class IndexModel : BaseIndexModel<RequestLog>
    {
        public string? ApplicationFilter { get; set; }
        
        public string? AspectFilter { get; set; }

        public bool BookmarkedFilter { get; set; }
    }
}
