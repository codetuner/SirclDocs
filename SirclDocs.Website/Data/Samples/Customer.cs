using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SirclDocs.Website.Data.Samples
{
    [Table(nameof(Customer), Schema = "samples")]
    public class Customer
    {
        [Key]
        [Column("CustomerID")]
        public int Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string CompanyName { get; set; }
        
        public string Phone { get; set; }
    }
}
