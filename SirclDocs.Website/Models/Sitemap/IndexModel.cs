using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SirclDocs.Website.Models.Sitemap
{
    public class IndexModel
    {
        public IndexModel(string rootUrl)
        {
            this.RootUrl = rootUrl;
        }

        public string RootUrl { get; private set; }

        public List<SitemapUrl> Urls { get; set; } = new();
    }
}
