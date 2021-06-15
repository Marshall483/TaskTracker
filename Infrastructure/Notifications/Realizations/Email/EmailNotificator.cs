using Infrastructure.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace Infrastructure.EmailNotifications
{
    class EmailNotificator : EmailNotificatorBase, INotificator<bool>
    {
        protected readonly Dictionary<RegistrationReason, Func<string, Task<bool>>> _notifyAboutRegistration;
        protected readonly Dictionary<ProjectReason, Func<string, Project, Task<bool>>> _notifyAboutProject;
        protected readonly Dictionary<TaskReason, Func<string, ProjectTask, Task<bool>>> _notifyAboutTask;

        public EmailNotificator(EmailSender sender) : base(sender) {

            _notifyAboutRegistration = new Dictionary<RegistrationReason, Func<string, Task<bool>>> {
                { RegistrationReason.Succeeded, base.AboutRegistrationSucceeded}
            };

            _notifyAboutProject = new Dictionary<ProjectReason, Func<string, Project, Task<bool>>> {
                { ProjectReason.Created, base.AboutProjectCreated },
                { ProjectReason.PriorityChanged, base.AboutPriorityChanged},
                { ProjectReason.StateChanged, base.AboutStateChanged }
            };

            _notifyAboutTask = new Dictionary<TaskReason, Func<string, ProjectTask, Task<bool>>>
            {
                { TaskReason.Created, base.AboutTaskCreated},
                { TaskReason.StateUpdated, base.AboutStateUpdated}
            };
        }

        public Task<bool> AboutRegistrationAsync(RegistrationReason reason, string email) =>
            _notifyAboutRegistration[reason].Invoke(email);

        public Task<bool> AboutProjectAsync(ProjectReason reason, string email, Project project) =>
            _notifyAboutProject[reason].Invoke(email, project);

        public Task<bool> AboutTaskAsync(TaskReason reason, string email, ProjectTask task) =>
            _notifyAboutTask[reason].Invoke(email, task);
    }
}
