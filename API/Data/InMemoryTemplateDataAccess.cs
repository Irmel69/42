using System.Collections.Concurrent;
using Contracts.DTOs;
using Contracts.Interfaces;

namespace API.Data;

public sealed class InMemoryTemplateDataAccess : ITemplateDataAccess
{
    private readonly ConcurrentDictionary<string, TaskTemplateDTO> templates = new();

    public Task<TaskTemplateDTO> SaveDataAsync(TaskTemplateDTO template)
    {
        templates[template.TaskID] = Clone(template);
        return Task.FromResult(Clone(template));
    }

    public Task<TaskTemplateDTO?> GetDataAsync(string taskId)
    {
        return Task.FromResult(templates.TryGetValue(taskId, out var template) ? Clone(template) : null);
    }

    public Task<IReadOnlyList<TaskTemplateDTO>> GetAllDataAsync()
    {
        IReadOnlyList<TaskTemplateDTO> result = templates.Values.Select(Clone).ToList();
        return Task.FromResult(result);
    }

    public Task<bool> UpdateDataAsync(TaskTemplateDTO template)
    {
        if (!templates.ContainsKey(template.TaskID))
        {
            return Task.FromResult(false);
        }

        templates[template.TaskID] = Clone(template);
        return Task.FromResult(true);
    }

    public Task<bool> DeleteDataAsync(string taskId)
    {
        return Task.FromResult(templates.TryRemove(taskId, out _));
    }

    private static TaskTemplateDTO Clone(TaskTemplateDTO template) => new()
    {
        TaskID = template.TaskID,
        Name = template.Name,
        Description = template.Description
    };
}
