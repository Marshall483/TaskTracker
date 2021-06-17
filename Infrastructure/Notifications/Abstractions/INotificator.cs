using Models;
using System.Threading.Tasks;

namespace Infrastructure.Notifications
{
    public enum RegistrationReason
    {
        Succeeded
    }

    public enum ProjectReason
    {
        Created,
        PriorityChanged,
        StateChanged,
    }

    public enum TaskReason
    {
        Created,
        StateUpdated
    }


    public interface INotificator<TResult>
    {
        public Task<TResult> AboutRegistrationAsync(RegistrationReason reson, string email);

        public Task<TResult> AboutProjectAsync(ProjectReason reson, string email, Project project);

        public Task<TResult> AboutTaskAsync(TaskReason reson, string email, ProjectTask task);

    }
}
