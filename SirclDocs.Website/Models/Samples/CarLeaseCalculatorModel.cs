using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SirclDocs.Website.Models.Samples
{
    public class CarLeaseCalculatorModel
    {
        public string Model { get; set; }

        public string Color { get; set; }

        public string[] Options { get; set; } = Array.Empty<string>();

        public int Duration { get; set; }

        public double DurationInYears => (this.Duration / 12.0);

        public decimal MonthlyPrice { get; internal set; }
    }
}
