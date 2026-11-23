using Contracts.DTOs;
using Contracts.Interfaces;

namespace BL.Services;

public sealed class TaskTemplateService(ITemplateDataAccess dataAccess) : ITaskTemplateService
{
    public async Task<bool> CreateNewTaskTemplate(string taskName, string taskDescription)
    {
        if (string.IsNullOrWhiteSpace(taskName) || string.IsNullOrWhiteSpace(taskDescription))
        {
            return false;
        }

        await dataAccess.SaveDataAsync(new TaskTemplateDTO
        {
            TaskID = Guid.NewGuid().ToString("N"),
            Name = taskName.Trim(),
            Description = taskDescription.Trim()
        });
        return true;
    }

    public Task<bool> DeleteTaskTemplate(string taskId) =>
        string.IsNullOrWhiteSpace(taskId) ? Task.FromResult(false) : dataAccess.DeleteDataAsync(taskId);

    public async Task<bool> UpdateTaskTemplate(string taskId, string taskName, string taskDescription)
    {
        if (string.IsNullOrWhiteSpace(taskId) || string.IsNullOrWhiteSpace(taskName) || string.IsNullOrWhiteSpace(taskDescription))
        {
            return false;
        }

        return await dataAccess.UpdateDataAsync(new TaskTemplateDTO
        {
            TaskID = taskId,
            Name = taskName.Trim(),
            Description = taskDescription.Trim()
        });
    }

    public async Task<List<TaskTemplateDTO>> GetAllTaskTemplates() => (await dataAccess.GetAllDataAsync()).ToList();

    public Task<TaskTemplateDTO?> GetTaskTemplate(string taskId) =>
        string.IsNullOrWhiteSpace(taskId) ? Task.FromResult<TaskTemplateDTO?>(null) : dataAccess.GetDataAsync(taskId);
}
