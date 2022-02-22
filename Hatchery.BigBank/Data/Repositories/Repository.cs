using System.Runtime.InteropServices;
using Hatchery.BigBank.Data.DataAccess;
using Hatchery.BigBank.Data.Entities;
using Hatchery.BigBank.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Hatchery.BigBank.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly ProjectContext _context;
        private readonly ILogger<Repository> _logger;

        public Repository(ProjectContext context, ILogger<Repository> logger) {
            _logger = logger;
            _context = context;
        }

        public void Add<T>(T entity) where T : class {
            _logger.LogInformation($"Adding {entity.GetType()} to the database");
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class {
            _logger.LogInformation($"Removing {entity.GetType()} from the database");
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync() {
            _logger.LogInformation("Attempting to save the changes to the content");
            return (await _context.SaveChangesAsync()) >= 0;
        }

        public async Task<Partner[]> GetAllPartnersAsync(bool includeCalculators = false) {
            _logger.LogInformation("Getting all Partners");
            IQueryable<Partner> query = _context.Partners;

            if (includeCalculators)
                query = query.Include(c => c.Calculators);
            return await query.ToArrayAsync();
        }

        public async Task<Partner> GetPartnerByCalculatorIdAsync(int calcId, bool includeCalculators = false) {
            _logger.LogInformation($"Getting Partner for calculator with id of {calcId}.");
            IQueryable<Partner> query = _context.Calculators.Where(c => c.LoanRequestId == calcId)
                .Select(p => p.Partner);

            if (includeCalculators)
                query = query.Include(c => c.Calculators);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Partner> GetPartnerByIBANAsync(string iban, bool includeCalculators = false)
        {
            _logger.LogInformation($"Getting partner with IBAN : {iban}");
            IQueryable<Partner> query = _context.Partners.Where(p => p.IBAN == iban);

            if (includeCalculators)
                query = query.Include(c => c.Calculators);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Partner> GetPartnerByIdAsync(int partnerId, bool includeCalculators = false) {
            _logger.LogInformation($"Getting Partner with id of {partnerId}.");
            IQueryable<Partner> query = _context.Partners.Where(c => c.PartnerId == partnerId);

            if (includeCalculators) query = query.Include(c => c.Calculators);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Partner[]> GetPartnersByPhoneNumberAsync(string phoneNumber, bool includeCalculators = false) {
            _logger.LogInformation($"Getting Partner for phone number : {phoneNumber}.");
            IQueryable<Partner> query = 
                _context.Calculators
                    .Where(c => c.PhoneNumber == phoneNumber)
                    .Select(p => p.Partner)
                    .Distinct();

            query = query.Include(c => c.Calculators);

            return await query.ToArrayAsync();
        }

        public async Task<LoanRequest[]> GetAllCalculatorsAsync(bool includePartners = false) {
            _logger.LogInformation("Getting all Calculators");
            IQueryable<LoanRequest> query = _context.Calculators;

            if (includePartners)
                query = query.Include(p => p.Partner);

            return await query.ToArrayAsync();
        }

        public async Task<LoanRequest> GetCalculatorByIdAsync(int calcId, bool includePartners = false) {
            _logger.LogInformation($"Getting Calculator for ID of {calcId}");
            IQueryable<LoanRequest> query = _context.Calculators
                .Where(c => c.LoanRequestId == calcId);

            if (includePartners)
                query = query.Include(p => p.Partner);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<LoanRequest[]> GetCalculatorByPhoneNumberAsync(string phoneNumber, bool includePartners = false) {
            _logger.LogInformation($"Getting Calculator for Phone number : {phoneNumber}.");
            IQueryable<LoanRequest> query = _context.Calculators.Where(p => p.PhoneNumber == phoneNumber);

            if (includePartners)
                query = query.Include(p => p.Partner);

            return await query.ToArrayAsync();
        }
    }
}
