using Hatchery.BigBank.Data.Entities;

namespace Hatchery.BigBank.Data.Repositories.Interfaces
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        #region Partner Setting
        Task<Partner[]> GetAllPartnersAsync(bool includeCalculators = false);
        Task<Partner> GetPartnerByCalculatorIdAsync(int calcId, bool includeCalculators = false);
        Task<Partner> GetPartnerByIBANAsync(string iban, bool includeCalculators = false);
        Task<Partner[]> GetPartnersByPhoneNumberAsync(string phoneNumber, bool includeCalculators = false);
        Task<Partner> GetPartnerByIdAsync(int partnerId, bool includeCalculators = false);
        #endregion

        #region Calculator Setting
        Task<LoanRequest[]> GetAllCalculatorsAsync(bool includePartners = false);
        Task<LoanRequest> GetCalculatorByIdAsync(int calcId, bool includePartners = false);
        Task<LoanRequest[]> GetCalculatorByPhoneNumberAsync(string phoneNumber, bool includePartners = false);
        #endregion

    }
}
