using ToDoTestApp.DTO;
using ToDoTestApp.Models;

namespace ToDoTestApp.Services.Contracts
{
    public interface IMyTaskService
    {
        Task<MyTaskDTO> GetTaskById(int id);
        Task<IEnumerable<MyTaskDTO>> GetTasksForUser(string userId, Filter? filter);
        Task<MyTaskDTO> AddTask(MyTaskDTO task, string userName);
        Task<MyTaskDTO> UpdateTask(int taskId, MyTaskDTO task);
        Task DeleteTask(int taskId);
    }
}
