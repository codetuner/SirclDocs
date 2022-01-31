using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SirclDocs.Website.Models.Samples
{
    public class TaskBoardModel
    {
        public int NextId { get; set; } = 1;

        public List<TaskBoardItem> Todos { get; } = new();

        public List<TaskBoardItem> Doings { get; } = new();

        public List<TaskBoardItem> Dones { get; } = new();

        public TaskBoardItem NewTask { get; set; } = new();
    }

    public class TaskBoardItem
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public int Estimate { get; set; }
    }
}
