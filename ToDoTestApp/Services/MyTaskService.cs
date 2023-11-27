using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ToDoTestApp.Data;
using ToDoTestApp.DTO;
using ToDoTestApp.Models;
using ToDoTestApp.Services.Contracts;

namespace ToDoTestApp.Services
{
    public class MyTaskService : IMyTaskService
    {
        private readonly ApplicationDbContext context;
        public MyTaskService(ApplicationDbContext dbContext)
        {
            this.context = dbContext;
        }

        public async Task<MyTaskDTO> GetTaskById(int id)
        {
            var task = await context.MyTasks.FindAsync(id);

            try
            {
                MyTaskDTO taskDTO = new MyTaskDTO()
                {
                    ID = task.ID,
                    Title = task.Title,
                    Done = task.Done,
                    Description = task.Description,
                    LevelOfImportance = task.LevelOfImportance
                };

                return taskDTO;
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<IEnumerable<MyTaskDTO>> GetTasksForUser(string userId, Filter? filter, string? sortBy)
        {
            var tasks = await context.MyTasks.Where(x => x.UserId == userId).ToListAsync();
            var tasksDTOs = new List<MyTaskDTO>();

            foreach (var task in tasks)
            {
                tasksDTOs.Add
                    (new MyTaskDTO()
                    {
                        ID = task.ID,
                        Title = task.Title,
                        Description = task.Description,
                        Done = task.Done,
                        LevelOfImportance = task.LevelOfImportance,
                        UserId = task.UserId
                    });
            }

            switch (sortBy)
            {
                case "title":
                    tasksDTOs.Sort((x, y) => string.Compare(x.Title, y.Title, StringComparison.OrdinalIgnoreCase));
                    break;
                case "levelOfImportance":
                    tasksDTOs.Sort((x, y) => y.LevelOfImportance.CompareTo(x.LevelOfImportance));
                    break;
                case "done":
                    tasksDTOs.Sort((x, y) => y.Done.CompareTo(x.Done));
                    break;
            }

            if (filter == null || filter.Done == null)
                return tasksDTOs;

            if(filter.LevelOfImportance != null)
            {
                tasksDTOs = tasksDTOs.Where(t => t.LevelOfImportance == filter.LevelOfImportance).ToList();
            }

            tasksDTOs = tasksDTOs.Where(t => t.Done == filter.Done).ToList();

            return tasksDTOs;
        }

        public async Task<MyTaskDTO> AddTask(MyTaskDTO taskDTO, string userId)
        {
            MyTask task = new MyTask()
            {
                ID = taskDTO.ID,
                Title = taskDTO.Title,
                Description = taskDTO.Description,
                Done = taskDTO.Done,
                LevelOfImportance = taskDTO.LevelOfImportance,
                UserId = userId
            };

            try
            {
                context.MyTasks.Add(task);
                await context.SaveChangesAsync();
                return taskDTO;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<MyTaskDTO> UpdateTask(int taskId, MyTaskDTO taskDTO)
        {
            MyTask task = await context.MyTasks.FirstOrDefaultAsync(x => x.ID == taskId);

            if (task == null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            if (task.Title != taskDTO.Title)
                task.Title = taskDTO.Title;

            if (task.Description != taskDTO.Description)
                task.Description = taskDTO.Description;

            if (task.LevelOfImportance != taskDTO.LevelOfImportance)
                task.LevelOfImportance = taskDTO.LevelOfImportance;

            if (task.Done != taskDTO.Done)
                task.Done = taskDTO.Done;

            task.ModifiedDate = DateTime.UtcNow;

            await context.SaveChangesAsync();

            return new MyTaskDTO()
            {
                ID = task.ID,
                Title = task.Title,
                Description = task.Description,
                Done = task.Done,
                LevelOfImportance = task.LevelOfImportance,
                UserId = task.UserId
            };
        }
        public async Task DeleteTask(int taskId)
        {
            var task = await context.MyTasks.FirstOrDefaultAsync(x => x.ID == taskId);
            if(task == null)
                throw new ArgumentNullException(nameof(task));

            context.MyTasks.Remove(task);
            await context.SaveChangesAsync();
        }

    }
}
