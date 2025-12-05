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

        public async Task<List<Account>> GetAllAsync(int companyID, bool includeCoinInfo = false)
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
            return retValue;
        }

        public async Task<List<Account>> GetByFilterAsync(int companyID, string data, bool includeCoinInfo = false)
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
            return retValue;
        }

        public async Task<Account?> GetByIDAsync(int companyID, string accountID, bool includeCoinInfo = false)
        {
            var retValue = (from a in context.Accounts
                            where a.AccountID == accountID
                            select a);
            if (includeCoinInfo)
            {
                retValue.Include(a => a.Coin);
            }
            return await retValue.FirstOrDefaultAsync();
        }

        public async Task AddAsync(Account account)
        {
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

        public async Task<int> UpdateAsync(Account account)
        {
            try
            {
                context.Accounts.Update(account);
                return await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeleteAsync(int companyID, string accountID)
        {
            try
            {
                return await context.Accounts
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
