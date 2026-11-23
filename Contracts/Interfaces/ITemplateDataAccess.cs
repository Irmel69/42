using Contracts.DTOs;

namespace Contracts.Interfaces;

public interface ITemplateDataAccess
{
    Task<TaskTemplateDTO> SaveDataAsync(TaskTemplateDTO template);
    Task<TaskTemplateDTO?> GetDataAsync(string taskId);
    Task<IReadOnlyList<TaskTemplateDTO>> GetAllDataAsync();
    Task<bool> UpdateDataAsync(TaskTemplateDTO template);
    Task<bool> DeleteDataAsync(string taskId);
}
