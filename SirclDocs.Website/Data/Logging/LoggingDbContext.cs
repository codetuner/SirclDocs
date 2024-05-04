using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SirclDocs.Website.Data.Logging
{
    public class LoggingDbContext : DbContext
    {
        static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();

        public LoggingDbContext(DbContextOptions<LoggingDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RequestLog>()
                .Property(e => e.Data)
                .HasConversion(
                v => JsonSerializer.Serialize(v, jsonSerializerOptions),
                s => JsonSerializer.Deserialize<Dictionary<string, string>>(s, jsonSerializerOptions),
                new ValueComparer<Dictionary<string, string>>(
                    (v1, v2) => String.Equals(JsonSerializer.Serialize(v1, jsonSerializerOptions), JsonSerializer.Serialize(v2, jsonSerializerOptions)),
                    v => JsonSerializer.Serialize(v, jsonSerializerOptions).GetHashCode(),
                    v => v.ToDictionary(p => p.Key, p => p.Value)
                )
            );

            modelBuilder.Entity<RequestLog>()
                .Property(e => e.Request)
                .HasConversion(
                j => JsonSerializer.Serialize(j, jsonSerializerOptions),
                s => JsonSerializer.Deserialize<Dictionary<string, string>>(s, jsonSerializerOptions),
                new ValueComparer<Dictionary<string, string>>(
                    (v1, v2) => String.Equals(JsonSerializer.Serialize(v1, jsonSerializerOptions), JsonSerializer.Serialize(v2, jsonSerializerOptions)),
                    v => JsonSerializer.Serialize(v, jsonSerializerOptions).GetHashCode(),
                    v => v.ToDictionary(p => p.Key, p => p.Value)
                )
            );
        }

        public DbSet<RequestLog> RequestLogs { get; set; }
    }
}
