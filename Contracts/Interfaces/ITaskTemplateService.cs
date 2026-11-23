using Contracts.DTOs;

namespace Contracts.Interfaces
{
    public interface ITaskTemplateService
    {
        public Task<bool> CreateNewTaskTemplate(string taskName, string taskDescription);
        public Task<bool> DeleteTaskTemplate(string taskId);
        public Task<bool> UpdateTaskTemplate(string taskId, string taskName, string taskDescription);
        public Task<List<TaskTemplateDTO>> GetAllTaskTemplates();
        public Task<TaskTemplateDTO?> GetTaskTemplate(string taskId);
    }
}
