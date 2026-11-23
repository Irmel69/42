using Contracts.DTOs;

namespace Contracts.Interfaces;

public interface IProtocolDataAccess
{
    Task<ProtocolEntryDTO> SaveDataAsync(ProtocolEntryDTO entry);
    Task<ProtocolEntryDTO?> GetDataAsync(string protocolId);
    Task<IReadOnlyList<ProtocolEntryDTO>> GetAllDataAsync();
    Task<bool> UpdateDataAsync(ProtocolEntryDTO entry);
    Task<bool> DeleteDataAsync(string protocolId);
}
