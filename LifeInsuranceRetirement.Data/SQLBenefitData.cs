using LifeInsuranceRetirement.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LifeInsuranceRetirement.Data
{
    public class SQLBenefitData : IBenefitData
    {
        private readonly LifeInsuranceRetirementDbContext db;
        private readonly IConfigurationData configurationData;
        private readonly IConsumerData consumerData;

        public SQLBenefitData(LifeInsuranceRetirementDbContext db, IConfigurationData configurationData, IConsumerData consumerData)
        {
            this.db = db;
            this.configurationData = configurationData;
            this.consumerData = consumerData;
        }

        public Benefit? Add(Benefit addBenefit)
        {
            var configuration = configurationData.Get();
            var consumer = consumerData.GetById(addBenefit.ConsumerId);
            if (consumer != null && configuration != null)
            {
                addBenefit.ConfigurationId = configuration.Id;
                addBenefit.Configuration = configuration;
                addBenefit.ConsumerId = consumer.Id;
                addBenefit.Consumer = consumer;

                db.Benefits.Add(addBenefit);
                ICollection<BenefitDetail> benefitDetails = CalculateBenefit(addBenefit);
                addBenefit.BenefitDetails = benefitDetails;
                AddBenefitDetails(benefitDetails);

                return addBenefit;
            }
            return null;
        }

        public async Task<Benefit?> AddAsync(Benefit addBenefit)
        {
            var configuration = await configurationData.GetAsync();
            var consumer = await consumerData.GetByIdAsync(addBenefit.ConsumerId);
            if (consumer != null && configuration != null)
            {
                addBenefit.ConfigurationId = configuration.Id;
                addBenefit.Configuration = configuration;
                addBenefit.ConsumerId = consumer.Id;
                addBenefit.Consumer = consumer;
                
                await db.Benefits.AddAsync(addBenefit);
                ICollection<BenefitDetail> benefitDetails = CalculateBenefit(addBenefit);
                addBenefit.BenefitDetails = benefitDetails;
                await AddBenefitDetailsAsync(benefitDetails);

                return addBenefit;
            }
            return null;
        }

        public void AddBenefitDetails(ICollection<BenefitDetail> addBenefitDetails)
        {
            if (addBenefitDetails?.Count() > 0)
            {
                foreach (BenefitDetail benefitDetail in addBenefitDetails)
                {
                    db.BenefitDetails.Add(benefitDetail);
                }
            }
        }

        public async Task AddBenefitDetailsAsync(ICollection<BenefitDetail> addBenefitDetails)
        {
            if (addBenefitDetails?.Count() > 0)
            {
                foreach (BenefitDetail benefitDetail in addBenefitDetails)
                {
                    await db.BenefitDetails.AddAsync(benefitDetail);
                }
            }
        }

        public void DeleteBenefitDetails(ICollection<BenefitDetail>? deleteBenefitDetails)
        {
            if (deleteBenefitDetails?.Count() > 0)
            {
                db.BenefitDetails.RemoveRange(deleteBenefitDetails);
            }
        }

        public Benefit? GetById(int id)
        {
            return db.Benefits.Include(b => b.BenefitDetails)
                .Include(b => b.Consumer)
                .FirstOrDefault(b => b.Id == id && !b.IsDeleted);
        }

        public async Task<Benefit?> GetByIdAsync(int id)
        {
            return await db.Benefits.Include(b => b.BenefitDetails)
                .Include(b => b.Consumer)
                .FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        }

        public Benefit? UpdateUpdatedByAndUpdatedDT(Benefit updateBenefit)
        {
            var benefit = GetById(updateBenefit.Id);

            if (benefit != null)
            {
                benefit.UpdatedBy = updateBenefit.UpdatedBy;
                benefit.UpdatedDT = updateBenefit.UpdatedDT;
            }

            return benefit;
        }

        public async Task<Benefit?> UpdateUpdatedByAndUpdatedDTAsync(Benefit updateBenefit)
        {
            var benefit = await GetByIdAsync(updateBenefit.Id);

            if (benefit != null)
            {
                benefit.UpdatedBy = updateBenefit.UpdatedBy;
                benefit.UpdatedDT = updateBenefit.UpdatedDT;
            }

            return benefit;
        }

        public Benefit? Delete(int id)
        {
            var benefit = GetById(id);
            if (benefit != null)
            {
                benefit.IsDeleted = true;
            }
            return benefit;
        }

        public async Task<Benefit?> DeleteAsync(int id)
        {
            var benefit = await GetByIdAsync(id);
            if (benefit != null)
            {
                benefit.IsDeleted = true;
            }
            return benefit;
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await db.SaveChangesAsync();
        }

        public ICollection<BenefitDetail> CalculateBenefit(Benefit benefit)
        {
            var calculateBenefits = new List<BenefitDetail>();
            for (int i = (benefit.Configuration?.MinRange ?? 1); i <= (benefit.Configuration?.MaxRange ?? 1); i += (benefit.Configuration?.Increments ?? 1))
            {
                var benefitDetailProp = new BenefitDetailProp
                {
                    Multiple = i,
                    BenefitId = benefit.Id,
                    Benefit = benefit
                };

                calculateBenefits.Add(new BenefitDetail()
                {
                    Multiple = benefitDetailProp.Multiple,
                    BenefitsAmountQuotation = benefitDetailProp.BenefitsAmountQuotation,
                    PendedAmount = benefitDetailProp.PendedAmount,
                    Status = benefitDetailProp.Status,
                    BenefitId = benefitDetailProp.BenefitId,
                    Benefit = benefitDetailProp.Benefit
                });
            }
            return calculateBenefits;
        }

        public Benefit? GetCalculatedBenefit(int consumerId)
        {
            var configuration = configurationData.Get();
            var consumer = consumerData.GetById(consumerId);
            
            if (consumer != null && configuration != null)
            {
                var newBenefit = new Benefit();
                newBenefit.ConfigurationId = configuration.Id;
                newBenefit.Configuration = configuration;
                newBenefit.ConsumerId = consumer.Id;
                newBenefit.Consumer = consumer;

                ICollection<BenefitDetail> benefitDetails = CalculateBenefit(newBenefit);
                newBenefit.BenefitDetails = benefitDetails;

                return newBenefit;
            }
            return null;
        }

        public async Task<Benefit?> GetCalculatedBenefitAsync(int consumerId)
        {
            var configuration = await configurationData.GetAsync();
            var consumer = await consumerData.GetByIdAsync(consumerId);
            
            if (consumer != null && configuration != null)
            {
                var newBenefit = new Benefit();
                newBenefit.ConfigurationId = configuration.Id;
                newBenefit.Configuration = configuration;
                newBenefit.ConsumerId = consumer.Id;
                newBenefit.Consumer = consumer;

                ICollection<BenefitDetail> benefitDetails = CalculateBenefit(newBenefit);
                newBenefit.BenefitDetails = benefitDetails;

                return newBenefit;
            }
            return null;
        }
    }
}