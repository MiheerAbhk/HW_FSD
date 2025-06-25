using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Models
{
    public enum Priority { Low, Medium, High }
    public enum Status { Pending, InProgress, Completed }

    public class TaskItem
    {
        public int TaskItemId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
    }

}
