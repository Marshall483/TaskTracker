using Models;
using System.Threading.Tasks;

namespace Infrastructure.Notifications
{
    public interface IRegistrationNotificator<TResult>
    {
        public Task<TResult> AboutRegistrationSucceeded(string email);
    }

    public interface IProjectNotificator<TResult>
    {
        public Task<TResult> AboutProjectCreated(string email, Project project);
        public Task<TResult> AboutStateChanged(string email, Project project);
        public Task<TResult> AboutPriorityChanged(string email, Project project);
    }

    public interface ITaskNotificator<TResult>
    {
        public Task<TResult> AboutTaskCreated(string email, ProjectTask task);
        public Task<TResult> AboutStateUpdated(string email, ProjectTask task);
    }
}
