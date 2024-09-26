using System.IO;

namespace SirclDocs.Website.Areas.MvcDashboardContent.Models.Media
{
    public class IndexModel
    {
        public string? Path { get; set; }
        
        public string? CurrentPathName { get; internal set; }
     
        public string? ParentPath { get; internal set; }
        
        public string? ParentPathName { get; internal set; }

        public string? DisplayPath { get; internal set; }

        public DirectoryInfo[] Directories { get; internal set; } = [];

        public FileInfo[] Files { get; internal set; } = [];
    }
}
