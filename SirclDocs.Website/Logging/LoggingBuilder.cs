using Microsoft.AspNetCore.Builder;

namespace SirclDocs.Website.Logging
{
    public class LoggingBuilder
    {
        public LoggingBuilder(IApplicationBuilder application)
        {
            this.Application = application;
        }

        public IApplicationBuilder Application { get; set; }
    }
}
