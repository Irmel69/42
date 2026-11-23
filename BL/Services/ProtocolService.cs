using Contracts.DTOs;
using Contracts.Interfaces;

namespace BL.Services;

public sealed class ProtocolService(IProtocolDataAccess dataAccess, ITaskTemplateService taskTemplateService) : IProtocolService
{
    public async Task<bool> CreateNewProtocolEntry(string taskID, string note)
    {
        if (string.IsNullOrWhiteSpace(taskID) || string.IsNullOrWhiteSpace(note))
        {
            return false;
        }

        await dataAccess.SaveDataAsync(new ProtocolEntryDTO
        {
            ProtocolID = Guid.NewGuid().ToString("N"),
            TaskID = taskID.Trim(),
            Note = note.Trim()
        });
        return true;
    }

    public Task<bool> DeleteProtocolEntry(string protocolId) =>
        string.IsNullOrWhiteSpace(protocolId) ? Task.FromResult(false) : dataAccess.DeleteDataAsync(protocolId);

    public async Task<bool> UpdateProtocolEntry(string protocolId, string note)
    {
        if (string.IsNullOrWhiteSpace(protocolId) || string.IsNullOrWhiteSpace(note))
        {
            return false;
        }

        var entry = await dataAccess.GetDataAsync(protocolId);
        if (entry is null)
        {
            return false;
        }

        entry.Note = note.Trim();
        return await dataAccess.UpdateDataAsync(entry);
    }

    public Task<ProtocolEntryDTO?> GetProtocolEntry(string protocolId) =>
        string.IsNullOrWhiteSpace(protocolId) ? Task.FromResult<ProtocolEntryDTO?>(null) : dataAccess.GetDataAsync(protocolId);

    public async Task<List<ProtocolEntryDTO>> GetAllProtocolEntries() => (await dataAccess.GetAllDataAsync()).ToList();

    public Task<bool> CreateNewTaskTemplate(string taskName, string taskDescription) => taskTemplateService.CreateNewTaskTemplate(taskName, taskDescription);
    public Task<bool> DeleteTaskTemplate(string taskId) => taskTemplateService.DeleteTaskTemplate(taskId);
    public Task<bool> UpdateTaskTemplate(string taskId, string taskName, string taskDescription) => taskTemplateService.UpdateTaskTemplate(taskId, taskName, taskDescription);
    public Task<List<TaskTemplateDTO>> GetAllTaskTemplates() => taskTemplateService.GetAllTaskTemplates();
    public Task<TaskTemplateDTO?> GetTaskTemplate(string taskId) => taskTemplateService.GetTaskTemplate(taskId);
}
