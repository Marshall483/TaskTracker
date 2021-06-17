using Models;
using System.Collections.Generic;

namespace Constructors
{
    public abstract class ProjectConstructorBase
    {
        public readonly Dictionary<ProjectState, string> _stateByEnumMap =
           new Dictionary<ProjectState, string>
           {
                {ProjectState.NotStarted, "Not started" },
                {ProjectState.Active, "Active" },
                {ProjectState.Completed, "Completed" }
           };

        public readonly Dictionary<string, ProjectState> _stateByStringMap =
          new Dictionary<string, ProjectState>
          {
                { "Not started", ProjectState.NotStarted },
                { "Active", ProjectState.Active },
                { "Completed", ProjectState.Completed }
          };

        public readonly Dictionary<int, string> _priorityByIntMap =
            new Dictionary<int, string>
            {
                { 1, "Low" },
                { 2, "Normal" },
                { 3, "High" },
            };

        public readonly Dictionary<string, int> _priorityByStringMap =
            new Dictionary<string, int>
            {
                { "Low", 1 },
                { "Normal", 2 },
                { "High", 3 },
            };

        public readonly Dictionary<TaskState, string> _taskStateByEnumMap =
            new Dictionary<TaskState, string>
            {
                { TaskState.ToDo, "ToDo" },
                { TaskState.InProgress, "In Progress" },
                { TaskState.Done, "Done" },
            };

        public readonly Dictionary<string, TaskState> _taskStateByStringMap =
            new Dictionary<string, TaskState>
            {
                { "ToDo", TaskState.ToDo },
                { "In Progress", TaskState.InProgress },
                { "Done", TaskState.Done },
            };
    }
}
