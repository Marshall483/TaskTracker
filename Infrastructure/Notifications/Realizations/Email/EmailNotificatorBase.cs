using System;
using System.Threading.Tasks;
using Models;
using Infrastructure.Notifications;

namespace Infrastructure.EmailNotifications
{
    public abstract class EmailNotificatorBase:
        IRegistrationNotificator<bool>,
        IProjectNotificator<bool>,
        ITaskNotificator<bool>
    {
        private protected EmailSender _sender;

        public EmailNotificatorBase(EmailSender sender) =>
            _sender = sender;


        /* Registration */
        public async Task<bool> AboutRegistrationSucceeded(string email) =>
           await SendNotify(email, $"Dear {email}, thank you for registration on task tracker!");


        /* About Projects */
        public async Task<bool> AboutProjectCreated(string email, Project project) =>
           await SendNotify(email, $"Dear {email}, project \"{project.ProjectName}\" juit created!");

        public async Task<bool> AboutStateChanged(string email, Project project) =>
           await SendNotify(email, $"Dear {email}, state in project \"{project.ProjectName}\" " +
               $"shanged state to {ToPretty(project.State)}!");

        public async Task<bool> AboutPriorityChanged(string email, Project project) =>
           await SendNotify(email, $"Dear {email},prioroty in project \"{project.ProjectName}\" " +
               $"shanged state to {ToPretty(project.Priority)}!");


        /* About Tasks */
        public async Task<bool> AboutTaskCreated(string email, ProjectTask task) =>
           await SendNotify(email, $"Dear {email}, a new task in your \"{task.Project.ProjectName}\" project" +
               $" with header {task.TaskName} juit created!");

        public async Task<bool> AboutStateUpdated(string email, ProjectTask task) =>
            await SendNotify(email, $"Dear {email}, a new task in your \"{task.Project.ProjectName}\" project" +
               $" with header {task.TaskName} have update state to {ToPretty(task.State)}!");


        private async Task<bool> SendNotify(string email, string message) =>
            await _sender.SendEmailAsync(email, "Notification", message);

        private string ToPretty(DateTime time) =>
            time.ToString("MMMM dd, yyyy");

        private string ToPretty(ProjectState state)
        {
            if (ProjectState.Active == state) return "Active";
            if (ProjectState.Completed == state) return "Completed";
            return "Created";
        }

        private string ToPretty(TaskState state)
        {
            if (TaskState.Done == state) return "Done";
            if (TaskState.InProgress == state) return "In progress";
            return "ToDo";
        }

        private string ToPretty(int prioroty)
        {
            if (prioroty == 1) return "Low";
            if (prioroty == 2) return "Normal";
            return "High";
        }
    }
}
