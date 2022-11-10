using LifeInsuranceRetirement.Core;
using Microsoft.EntityFrameworkCore;

namespace LifeInsuranceRetirement.Data
{
    public class SQLConsumerData : IConsumerData
    {
        private readonly LifeInsuranceRetirementDbContext db;
        private readonly IConfigurationData configurationData;

        public SQLConsumerData(LifeInsuranceRetirementDbContext db, IConfigurationData configurationData)
        {
            this.db = db;
            this.configurationData = configurationData;
        }

        public IEnumerable<Consumer> GetConsumersByName(string name)
        {
            return db.Consumers.Where(c => (c.Name != null && c.Name.StartsWith(name)) || string.IsNullOrEmpty(name)).ToList();
        }

        public Consumer GetById(int id)
        {
            var consumer = db.Consumers.Include(c => c.Benefits).FirstOrDefault(c => c.Id == id);
            if (consumer == null)
            {
                consumer = new Consumer();
            }
            return consumer;
        }

        public Consumer Add(Consumer addConsumer)
        {
            db.Consumers.Add(addConsumer);

            db.SaveChanges();

            var calculatedConsumer = CalculateBenefits(addConsumer);

            return calculatedConsumer;
        }

        public Consumer? Update(Consumer updateConsumer)
        {
            var consumer = GetById(updateConsumer.Id);

            if (consumer != null)
            {
                db.Entry(consumer).State = EntityState.Detached;

                var entity = db.Consumers.Attach(updateConsumer);
                entity.State = EntityState.Modified;

                db.SaveChanges();

                var calculatedConsumer = CalculateBenefits(updateConsumer);

                return calculatedConsumer;
            }

            return consumer;
        }

        public Consumer? Delete(int id)
        {
            var consumer = GetById(id);

            if (consumer != null)
            {
                if (consumer.Benefits?.Count() > 0)
                {
                    db.Benefits.RemoveRange(consumer.Benefits);
                }

                db.Consumers.Remove(consumer);
                db.SaveChanges();
            }

            return consumer;
        }

        public Consumer CalculateBenefits(Consumer calculateConsumer)
        {
            var configuration = configurationData.Get();
            var consumer = GetById(calculateConsumer.Id);

            if (consumer.Benefits?.Count() > 0)
            {
                db.Benefits.RemoveRange(consumer.Benefits);
                db.SaveChanges();
            }

            var newBenefits = new List<Benefits>();

            for (int i = (configuration.MinRange ?? 1); i <= (configuration.MaxRange ?? 1); i += (configuration.Increments ?? 1))
            {
                newBenefits.Add(new Benefits()
                {
                    Multiple = i,
                    ConfigurationId = configuration.Id,
                    ConsumerId = consumer.Id
                });
            }

            consumer.Benefits = newBenefits;

            db.SaveChanges();

            return consumer;
        }

        public async Task<IEnumerable<Consumer>> GetConsumersByNameAsync(string name)
        {
            return await db.Consumers.Where(c => (c.Name != null && c.Name.StartsWith(name)) || string.IsNullOrEmpty(name)).ToListAsync();
        }

        public async Task<Consumer?> GetByIdAsync(int id)
        {
            var consumer = await db.Consumers.Include(c => c.Benefits).FirstOrDefaultAsync(c => c.Id == id);
            return consumer;
        }

        public async Task<Consumer> AddAsync(Consumer addConsumer)
        {
            await db.Consumers.AddAsync(addConsumer);

            await db.SaveChangesAsync();

            var calculatedConsumer = await CalculateBenefitsAsync(addConsumer);

            return calculatedConsumer;
        }

        public async Task<Consumer?> UpdateAsync(Consumer updateConsumer)
        {
            var consumer = await GetByIdAsync(updateConsumer.Id);

            if (consumer != null)
            {
                db.Entry(consumer).State = EntityState.Detached;

                var entity = db.Consumers.Attach(updateConsumer);
                entity.State = EntityState.Modified;

                await db.SaveChangesAsync();

                var calculatedConsumer = await CalculateBenefitsAsync(updateConsumer);

                return calculatedConsumer;
            }

            return consumer;
        }

        public async Task<Consumer?> DeleteAsync(int id)
        {
            var consumer = await GetByIdAsync(id);

            if (consumer != null)
            {
                if (consumer.Benefits?.Count() > 0)
                {
                    db.Benefits.RemoveRange(consumer.Benefits);
                }

                db.Consumers.Remove(consumer);
                await db.SaveChangesAsync();
            }

            return consumer;
        }

        public async Task<Consumer> CalculateBenefitsAsync(Consumer calculateConsumer)
        {
            var configuration = await configurationData.GetAsync();
            var consumer = await GetByIdAsync(calculateConsumer.Id);

            if (consumer != null)
            {
                if (consumer.Benefits?.Count() > 0)
                {
                    db.Benefits.RemoveRange(consumer.Benefits);
                    db.SaveChanges();
                }

                var newBenefits = new List<Benefits>();

                for (int i = (configuration.MinRange ?? 1); i <= (configuration.MaxRange ?? 1); i += (configuration.Increments ?? 1))
                {
                    newBenefits.Add(new Benefits()
                    {
                        Multiple = i,
                        ConfigurationId = configuration.Id,
                        ConsumerId = consumer.Id
                    });
                }

                consumer.Benefits = newBenefits;
                await db.SaveChangesAsync();

                return consumer;
            }

            return calculateConsumer;
        }
    }
}
