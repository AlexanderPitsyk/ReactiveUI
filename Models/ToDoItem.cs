using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ReactiveUIApplication.Common;

namespace ReactiveUIApplication.Models
{
    public class ToDoItem
    {
        [Key]
        public int ToDoItemId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public DateTime Created { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsDone { get; set; }

        [Required]
        public int PriorityId { get; set; }

        [NotMapped]
        public PriorityOption Priority => Extentions.GetEnumByIndex<PriorityOption>(PriorityId);

        [NotMapped]
        public ObservableCollection<PriorityOption> Collection { get; set; }

        [NotMapped]
        public PriorityOption PriorityItem
        {
            get { return Priority; }
            set { PriorityId = (int)value; }
        }
    }

    public enum PriorityOption
    {
        Default,
        Minor,
        Major,
        Critical
    }
}