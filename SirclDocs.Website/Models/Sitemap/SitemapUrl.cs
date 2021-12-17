using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SirclDocs.Website.Models.Sitemap
{
    public class SitemapUrl
    {
        public SitemapUrl(string location, DateTime? lastModified = null)
        {
            this.Location = location;
            this.LastModified = lastModified;
        }

        public string Location { get; private set; }

        public DateTime? LastModified { get; private set; }
    }
}
