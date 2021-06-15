using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Models
{
    public enum TaskState : byte
    { 
        ToDo,
        InProgress,
        Done
    }


    public class ProjectTask
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string TaskName { get; set; }
        public ICollection<TaskFields> Fields { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        [Required]
        public TaskState State { get; set; }

        [NotMapped]
        public bool AnyFields =>
            Fields != null &&
            Fields.Any();
    }

    public class TaskFields
    {
        public Guid Id { get; set; }

        public Guid TaskId { get; set; }
        public ProjectTask Task { get; set; }

        public string Description { get; set; }
    }
}
