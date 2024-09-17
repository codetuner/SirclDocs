using System.Collections.Generic;

namespace SirclDocs.Website.Areas.MvcDashboardContent.Models.PathRedirection
{
    public class EditModel : BaseEditModel<Data.Content.PathRedirection>
    {
        public bool IsNew => this.Item.Id == 0;

        public List<string> PathsList { get; internal set; } = [];
    }
}
