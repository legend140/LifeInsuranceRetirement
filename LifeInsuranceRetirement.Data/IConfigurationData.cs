using LifeInsuranceRetirement.Core;

namespace LifeInsuranceRetirement.Data
{
    public interface IConfigurationData
    {
        Configuration Get();
        Configuration Save(Configuration savedConfiguration);

        Task<Configuration> GetAsync();
        Task<Configuration> SaveAsync(Configuration savedConfiguration);
    }
}