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

        public Configuration Get()
        {
            var configuration = db.Configuration.FirstOrDefault();
            if (configuration == null)
            {
                configuration = new Configuration();
            }
            return configuration;
        }

        public Configuration Save(Configuration savedConfiguration)
        {
            var checkConfiguration = Get();
            savedConfiguration.Id = checkConfiguration.Id;

            db.Entry(checkConfiguration).State = EntityState.Detached;

            if (checkConfiguration.Id == 0)
            {
                db.Configuration.Add(savedConfiguration);
            } else
            {
                var entity = db.Configuration.Attach(savedConfiguration);
                entity.State = EntityState.Modified;
            }

            db.SaveChanges();

            return savedConfiguration;
        }

        public async Task<Configuration> GetAsync()
        {
            var configuration = await db.Configuration.FirstOrDefaultAsync();
            if (configuration == null)
            {
                configuration = new Configuration();
            }
            return configuration;
        }

        public async Task<Configuration> SaveAsync(Configuration savedConfiguration)
        {
            var checkConfiguration = await GetAsync();
            savedConfiguration.Id = checkConfiguration.Id;

            db.Entry(checkConfiguration).State = EntityState.Detached;

            if (checkConfiguration.Id == 0)
            {
                await db.Configuration.AddAsync(savedConfiguration);
            }
            else
            {
                var entity = db.Configuration.Attach(savedConfiguration);
                entity.State = EntityState.Modified;
            }

            await db.SaveChangesAsync();

            return savedConfiguration;
        }
    }
}