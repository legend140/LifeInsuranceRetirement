using LifeInsuranceRetirement.Core;
using Microsoft.EntityFrameworkCore;

namespace LifeInsuranceRetirement.Data
{
    public class SQLConfigurationData : IConfigurationData
    {
        private readonly LifeInsuranceRetirementDbContext db;

        public SQLConfigurationData(LifeInsuranceRetirementDbContext db)
        {
            this.db = db;
        }

        public Configuration? Get()
        {
            return db.Configuration.FirstOrDefault(c => !c.IsDeleted);
        }

        public Configuration Add(Configuration addConfiguration)
        {
            db.Configuration.Add(addConfiguration);
            return addConfiguration;
        }

        public Configuration? Update(Configuration updateConfiguration)
        {
            var checkConfiguration = Get();
            if (checkConfiguration != null)
            {
                db.Entry(checkConfiguration).State = EntityState.Detached;

                updateConfiguration.Id = checkConfiguration.Id;
                updateConfiguration.CreatedBy = checkConfiguration.CreatedBy;
                updateConfiguration.CreatedDT = checkConfiguration.CreatedDT;

                var entity = db.Configuration.Attach(updateConfiguration);
                entity.State = EntityState.Modified;

                checkConfiguration = updateConfiguration;
            }

            return checkConfiguration;
        }

        public IEnumerable<ConfigurationLogs> GetLogs(int configurationId)
        {
            return db.ConfigurationLogs.Where(l => l.ConfigurationId == configurationId).ToList();
        }

        public ConfigurationLogs AddLogs(ConfigurationLogs addConfigurationLogs)
        {
            db.ConfigurationLogs.Add(addConfigurationLogs);
            return addConfigurationLogs;
        }

        public async Task<Configuration?> GetAsync()
        {
            return await db.Configuration
                .FirstOrDefaultAsync(c => !c.IsDeleted);
        }

        public async Task<Configuration> AddAsync(Configuration addConfiguration)
        {
            await db.Configuration.AddAsync(addConfiguration);
            return addConfiguration;
        }

        public async Task<Configuration?> UpdateAsync(Configuration updateConfiguration)
        {
            var checkConfiguration = await GetAsync();
            if (checkConfiguration != null)
            {
                db.Entry(checkConfiguration).State = EntityState.Detached;

                updateConfiguration.Id = checkConfiguration.Id;
                updateConfiguration.CreatedBy = checkConfiguration.CreatedBy;
                updateConfiguration.CreatedDT = checkConfiguration.CreatedDT;

                var entity = db.Configuration.Attach(updateConfiguration);
                entity.State = EntityState.Modified;

                checkConfiguration = updateConfiguration;
            }

            return checkConfiguration;
        }

        public async Task<IEnumerable<ConfigurationLogs>> GetLogsAsync(int configurationId)
        {
            return await db.ConfigurationLogs.Where(l => l.ConfigurationId == configurationId).ToListAsync();
        }

        public async Task<ConfigurationLogs> AddLogsAsync(ConfigurationLogs addConfigurationLogs)
        {
            await db.ConfigurationLogs.AddAsync(addConfigurationLogs);
            return addConfigurationLogs;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await db.SaveChangesAsync();
        }
    }
}