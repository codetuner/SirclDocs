using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SirclDocs.Website.Data.Samples
{
    public class SamplesDbContext : DbContext
    {
        public SamplesDbContext(DbContextOptions<SamplesDbContext> options)
            : base(options)
        { }

        public DbSet<Samples.Customer> Customers { get; set; }
    }
}
