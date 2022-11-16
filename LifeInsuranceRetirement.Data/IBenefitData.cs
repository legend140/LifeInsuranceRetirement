using LifeInsuranceRetirement.Core;

namespace LifeInsuranceRetirement.Data
{
    public interface IBenefitData
    {
        Benefit? GetCalculatedBenefit(int consumerId);
        Benefit? GetById(int id);
        Benefit? Add(Benefit addBenefit);
        Benefit? UpdateUpdatedByAndUpdatedDT(Benefit updateDate);
        Benefit? Delete(int id);
        void AddBenefitDetails(ICollection<BenefitDetail> addBenefitDetails);
        void DeleteBenefitDetails(ICollection<BenefitDetail>? deleteBenefitDetails);
        ICollection<BenefitDetail> CalculateBenefit(Benefit benefit);
        int Commit();

        Task<Benefit?> GetCalculatedBenefitAsync(int consumerId);
        Task<Benefit?> GetByIdAsync(int id);
        Task<Benefit?> AddAsync(Benefit addBenefit);
        Task<Benefit?> UpdateUpdatedByAndUpdatedDTAsync(Benefit updateDate);
        Task<Benefit?> DeleteAsync(int id);
        Task AddBenefitDetailsAsync(ICollection<BenefitDetail> addBenefitDetails);
        Task<int> CommitAsync();
    }
}
