using Accounting.Domain.DTO;
using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure.Repository
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly AccountingDbContext context;

        public AccountsRepository(AccountingDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<AccountDTO>> GetAllAsync(int companyID, bool includeCoinInfo = false)
        {
            List<Account> retValue;
            IQueryable<Account> query = context.Accounts.Where(a => a.CompanyID == companyID);
            if (includeCoinInfo)
            {
                retValue = await query.Include(a => a.Coin).ToListAsync();
            }
            else
            {
                retValue = await query.ToListAsync();
            }
            return retValue.Select(a => Mappers.DtoMappers.MapAccountToDTO(a)).ToList();
        }

        public async Task<List<AccountDTO>> GetByFilterAsync(int companyID, string data, bool includeCoinInfo = false)
        {
            List<Account> retValue;
            IQueryable<Account> query = (from a in context.Accounts
                                         where a.CompanyID == companyID
                                         && (a.AccountID.Contains(data) || a.Description.Contains(data))
                                         select a);
                context.Accounts.Where(a => a.CompanyID == companyID);
            if (includeCoinInfo)
            {
                retValue = await query.Include(a => a.Coin).ToListAsync();
            }
            else
            {
                retValue = await query.ToListAsync();
            }
            return retValue.Select(a => Mappers.DtoMappers.MapAccountToDTO(a)).ToList();
        }

        public async Task<AccountDTO?> GetByIDAsync(int companyID, string accountID, bool includeCoinInfo = false)
        {
            var query = (from a in context.Accounts
                           where a.AccountID == accountID
                           select a);
            if (includeCoinInfo)
            {
                query.Include(a => a.Coin);
            }
            Account? account = await query.FirstOrDefaultAsync();
            return (account == null) ? null : Mappers.DtoMappers.MapAccountToDTO(account);
        }

        public async Task AddAsync(AccountDTO accountDTO)
        {
            Account account = Mappers.EntityMappers.MapToAccount(accountDTO);
            try
            {
                await Task.Run(() =>
                    {
                        context.Accounts.AddAsync(account);
                        context.SaveChangesAsync();
                    }
                );
            }
            catch {
                throw;
            }
        }

        public async Task UpdateAsync(AccountDTO accountDTO)
        {
            try
            {
                Account account = Mappers.EntityMappers.MapToAccount(accountDTO);
                context.Accounts.Update(account);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAsync(int companyID, string accountID)
        {
            try
            {
                await context.Accounts
                             .Where(e => e.CompanyID == companyID && e.AccountID == accountID)
                             .ExecuteDeleteAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
