using System;

namespace TaskManager.API.Models
{
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Priority Priority { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }

    public enum Priority
    {
        Low,
        Medium,
        High
    }
} 