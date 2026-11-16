namespace Contracts
{
    internal interface IProtocolService
    {
        #region Methods
        public Task<bool> CreateNewProtocolEntry(string taskID, string note);

        public Task<bool> DeleteProtocolEntry(string protocolId);

        public Task<bool> UpdateProtocolEntry(string protocolId, string note);

        public Task<ProtocolEntryDTO> GetProtocolEntry(string protocolId);
        public Task<List<ProtocolEntryDTO>> GetAllProtocolEntries();
        public Task<bool> CreateNewTaskTemplate(string taskName, string taskDescription);

        public Task<bool> DeleteTaskTemplate(string taskId);

        public Task<bool> UpdateTaskTemplate(string taskId, string taskName, string taskDescription);

        public Task<List<TaskTemplateDTO>> GetAllTaskTemplates();

        public Task<TaskTemplateDTO> GetTaskTemplate(string taskId);
        #endregion
    }
}
