using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SirclDocs.Website.Models.Samples
{
    public class ExpenseModel
    {
        public Expense Item { get; set; }

        public ExpenseLine CurrentLine { get; set; }

        public List<SelectListItem> Managers { get; internal set; }

        public List<SelectListItem> CostCenters { get; internal set; }

        public List<SelectListItem> Categories { get; internal set; }
    }

    public class Expense
    {
        [Required]
        public string Description { get; set; }

        [Required]
        public string CostCenter { get; set; }

        [Required]
        public string Manger { get; set; }

        public int PaymentMethod { get; set; }

        public List<ExpenseLine> Lines { get; set; } = new();

        public decimal TotalAmount => Lines.Sum(l => l.Amount ?? 0m);
    }

    public class ExpenseLine
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public decimal? Amount { get; set; }

        [Required]
        public string Category { get; set; }

        public List<string> Names { get; set; } = new();
    }
}
