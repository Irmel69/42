using System;

namespace Contracts.Interfaces;

public interface IProtocolDataAccess
{
    public Task AsyncSaveData(string guid);

    public Task AsyncGetData(string guid);

    public Task AsyncUpdateData(string guid);

    public Task AsyncGetAllData();

    public Task AsyncDeleteData(string guid);
}
