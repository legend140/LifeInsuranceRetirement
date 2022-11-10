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
        Consumer CalculateBenefits(Consumer calculateConsumer);

        Task<IEnumerable<Consumer>> GetConsumersByNameAsync(string name);
        Task<Consumer?> GetByIdAsync(int id);
        Task<Consumer> AddAsync(Consumer addConsumer);
        Task<Consumer?> UpdateAsync(Consumer updateConsumer);
        Task<Consumer?> DeleteAsync(int id);
        Task<Consumer> CalculateBenefitsAsync(Consumer calculateConsumer);
    }
}
