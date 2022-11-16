using LifeInsuranceRetirement.Core;

namespace LifeInsuranceRetirement.Data
{
    public interface IConfigurationData
    {
        Configuration? Get();
        Configuration Add(Configuration addConfiguration);
        Configuration? Update(Configuration updateConfiguration);
        IEnumerable<ConfigurationLogs> GetLogs(int configurationId);
        ConfigurationLogs AddLogs(ConfigurationLogs addConfigurationLogs);
        int Commit();

        Task<Configuration?> GetAsync();
        Task<Configuration> AddAsync(Configuration addConfiguration);
        Task<Configuration?> UpdateAsync(Configuration updateConfiguration);
        Task<IEnumerable<ConfigurationLogs>> GetLogsAsync(int configurationId);
        Task<ConfigurationLogs> AddLogsAsync(ConfigurationLogs addConfigurationLogs);
        Task<int> CommitAsync();
    }
}