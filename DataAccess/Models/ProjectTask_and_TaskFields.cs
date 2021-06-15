using System;
using System.Collections.Generic;
using System.Text;

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
        public Guid Id { get; set; }

        public string Header { get; set; }
        public string Description { get; set; }

        public Guid ProjectId { get; set; }
        public Project Project { get; set; }

        public TaskState State { get; set; }
    }

    public class TaskFields
    {
        public Guid Id { get; set; }

        public Guid TaskId { get; set; }
        public ProjectTask Task { get; set; }

        public string Content { get; set; }
    }
}
