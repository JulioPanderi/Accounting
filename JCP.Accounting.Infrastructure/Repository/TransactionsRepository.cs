using Accounting.Infrastructure.Filters;
using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace Accounting.Infrastructure.Repository
{
    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly AccountingDbContext context;

        public TransactionsRepository(AccountingDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Transaction>> GetByFiltersAsync(int companyID, TransactionsFilter filter)
        {
            List<Transaction> retValue;
            TransactionRespositoryHelper helper = new TransactionRespositoryHelper(context);
            IQueryable<Transaction> query = helper.GetByFilter(companyID, filter);
            retValue = await query.ToListAsync();
            return retValue;
        }

        public async Task<Transaction?> GetByIDAsync(long transactionID)
        {
            var retValue = (from t in context.Transactions
                            where t.TransactionID == transactionID
                            select t);
            return await retValue.FirstOrDefaultAsync();
        }

        public async Task<long> AddAsync(Transaction transaction)
        {
            try
            {
                long retValue = 0;
                await Task.Run(async () =>
                    {
                        context.Transactions.Add(transaction);
                        await context.SaveChangesAsync();
                        retValue = transaction.TransactionID;
                    }
                );
                return retValue;
            }
            catch {
                throw;
            }
        }

        public async Task<int> UpdateAsync(Transaction transaction)
        {
            try
            {
                //TODO: implement update, recording auditory registry and mark transaction as modified
                throw new InvalidOperationException("Transaction update is not allowed.");
                
                //context.Transactions.Update(transaction);
                //return await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<int> DeleteAsync(Transaction transaction)
        {
            //TODO: implement delete, recording auditory registry and mark transaction as eliminated
            throw new InvalidOperationException("Transaction delete is not allowed.");

            //context.Transactions.Update(transaction);
            //return await context.SaveChangesAsync();
        }
    }
}
