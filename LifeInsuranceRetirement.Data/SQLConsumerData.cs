using LifeInsuranceRetirement.Core;
using Microsoft.EntityFrameworkCore;

namespace LifeInsuranceRetirement.Data
{
    public class SQLConsumerData : IConsumerData
    {
        private readonly LifeInsuranceRetirementDbContext db;

        public SQLConsumerData(LifeInsuranceRetirementDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Consumer> GetConsumersByName(string name)
        {
            return db.Consumers.Where(c => !c.IsDeleted && ((c.Name != null && c.Name.StartsWith(name)) || string.IsNullOrEmpty(name))).ToList();
        }

        public Consumer GetById(int id)
        {
            var consumer = db.Consumers
                .Include(c => c.Benefit)
                .ThenInclude(b => b.BenefitDetails)
                .FirstOrDefault(c => c.Id == id && !c.IsDeleted);

            if (consumer == null)
            {
                consumer = new Consumer();
            }
            return consumer;
        }

        public Consumer Add(Consumer addConsumer)
        {
            db.Consumers.Add(addConsumer);
            return addConsumer;
        }

        public Consumer? Update(Consumer updateConsumer)
        {
            var consumer = GetById(updateConsumer.Id);

            if (consumer != null)
            {
                db.Entry(consumer).State = EntityState.Detached;
                updateConsumer.Id = consumer.Id;
                updateConsumer.CreatedBy = consumer.CreatedBy;
                updateConsumer.CreatedDT = consumer.CreatedDT;
                var entity = db.Consumers.Attach(updateConsumer);
                entity.State = EntityState.Modified;
                consumer = updateConsumer;
            }

            return consumer;
        }

        public Consumer? Delete(int id)
        {
            var consumer = GetById(id);
            if (consumer != null)
            {
                consumer.IsDeleted = true;
            }
            return consumer;
        }

        public IEnumerable<ConsumerLogs> GetLogs(int consumerId)
        {
            return db.ConsumerLogs
                .Include(l => l.Benefit)
                .ThenInclude(b => b.BenefitDetails)
                .Where(l => l.ConsumerId == consumerId)
                .ToList();
        }

        public ConsumerLogs AddLogs(ConsumerLogs addConsumerLogs)
        {
            db.ConsumerLogs.Add(addConsumerLogs);
            return addConsumerLogs;
        }

        public async Task<IEnumerable<Consumer>> GetConsumersByNameAsync(string name)
        {
            return await db.Consumers.Where(c => !c.IsDeleted && ((c.Name != null && c.Name.StartsWith(name)) || string.IsNullOrEmpty(name))).ToListAsync();
        }

        public async Task<Consumer?> GetByIdAsync(int id)
        {
            return await db.Consumers
                .Include(c => c.Benefit)
                .ThenInclude(b => b.BenefitDetails)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<Consumer> AddAsync(Consumer addConsumer)
        {
            await db.Consumers.AddAsync(addConsumer);
            return addConsumer;
        }

        public async Task<Consumer?> UpdateAsync(Consumer updateConsumer)
        {
            var consumer = await GetByIdAsync(updateConsumer.Id);

            if (consumer != null)
            {
                db.Entry(consumer).State = EntityState.Detached;
                updateConsumer.Id = consumer.Id;
                updateConsumer.CreatedBy = consumer.CreatedBy;
                updateConsumer.CreatedDT = consumer.CreatedDT;
                var entity = db.Consumers.Attach(updateConsumer);
                entity.State = EntityState.Modified;

                consumer = updateConsumer;
            }

            return consumer;
        }

        public async Task<Consumer?> DeleteAsync(int id)
        {
            var consumer = await GetByIdAsync(id);
            if (consumer != null)
            {
                consumer.IsDeleted = true;
            }
            return consumer;
        }

        public async Task<IEnumerable<ConsumerLogs>> GetLogsAsync(int consumerId)
        {
            return await db.ConsumerLogs
                .Include(l => l.Benefit)
                .ThenInclude(b => b.BenefitDetails)
                .Where(l => l.ConsumerId == consumerId)
                .ToListAsync();
        }

        public async Task<ConsumerLogs> AddLogsAsync(ConsumerLogs addConsumerLogs)
        {
            await db.ConsumerLogs.AddAsync(addConsumerLogs);
            return addConsumerLogs;
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
