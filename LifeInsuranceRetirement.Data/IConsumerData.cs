using LifeInsuranceRetirement.Core;

namespace LifeInsuranceRetirement.Data
{
    public interface IConsumerData
    {
        IEnumerable<Consumer> GetConsumersByName(string name);
        Consumer GetById(int id);
        Consumer Add(Consumer addConsumer);
        Consumer? Update(Consumer updateConsumer);
        Consumer? Delete(int id);
        IEnumerable<ConsumerLogs> GetLogs(int consumerId);
        ConsumerLogs AddLogs(ConsumerLogs addConsumerLogs);
        int Commit();

        Task<IEnumerable<Consumer>> GetConsumersByNameAsync(string name);
        Task<Consumer?> GetByIdAsync(int id);
        Task<Consumer> AddAsync(Consumer addConsumer);
        Task<Consumer?> UpdateAsync(Consumer updateConsumer);
        Task<Consumer?> DeleteAsync(int id);
        Task<IEnumerable<ConsumerLogs>> GetLogsAsync(int consumerId);
        Task<ConsumerLogs> AddLogsAsync(ConsumerLogs addConsumerLogs);
        Task<int> CommitAsync();
    }
}
