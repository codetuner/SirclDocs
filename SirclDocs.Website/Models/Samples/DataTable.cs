using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SirclDocs.Website.Models.Samples
{
    public class DataTable<T>
    {
        public T[] Items { get; set; }

        public int LastPage { get; set; }
        
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string Query { get; set; }

        public int[] Selection { get; set; }
    }
}
