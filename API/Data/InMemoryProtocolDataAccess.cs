using System.Collections.Concurrent;
using Contracts.DTOs;
using Contracts.Interfaces;

namespace API.Data;

public sealed class InMemoryProtocolDataAccess : IProtocolDataAccess
{
    private readonly ConcurrentDictionary<string, ProtocolEntryDTO> entries = new();

    public Task<ProtocolEntryDTO> SaveDataAsync(ProtocolEntryDTO entry)
    {
        entries[entry.ProtocolID] = Clone(entry);
        return Task.FromResult(Clone(entry));
    }

    public Task<ProtocolEntryDTO?> GetDataAsync(string protocolId)
    {
        return Task.FromResult(entries.TryGetValue(protocolId, out var entry) ? Clone(entry) : null);
    }

    public Task<IReadOnlyList<ProtocolEntryDTO>> GetAllDataAsync()
    {
        IReadOnlyList<ProtocolEntryDTO> result = entries.Values.Select(Clone).ToList();
        return Task.FromResult(result);
    }

    public Task<bool> UpdateDataAsync(ProtocolEntryDTO entry)
    {
        if (!entries.ContainsKey(entry.ProtocolID))
        {
            return Task.FromResult(false);
        }

        entries[entry.ProtocolID] = Clone(entry);
        return Task.FromResult(true);
    }

    public Task<bool> DeleteDataAsync(string protocolId)
    {
        return Task.FromResult(entries.TryRemove(protocolId, out _));
    }

    private static ProtocolEntryDTO Clone(ProtocolEntryDTO entry) => new()
    {
        ProtocolID = entry.ProtocolID,
        TaskID = entry.TaskID,
        Note = entry.Note,
        Name = entry.Name
    };
}
