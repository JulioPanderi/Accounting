using Accounting.Domain.DTO;
using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;
using Accounting.Shared.Enums;
using Accounting.Shared.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Accounting.Infrastructure.Repository
{
    internal class TransactionsRepository : ITransactionsRepository
    {
        private readonly AccountingDbContext context;
        private readonly Dictionary<RelatedTransactionEntity, string> RelatedEntities;
        private readonly DateTime currentDate = DateTime.Now;

        public TransactionsRepository(AccountingDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            #region Related Transaction entities dictionary
            RelatedEntities = new Dictionary<RelatedTransactionEntity, string>();
            RelatedEntities.Add(RelatedTransactionEntity.JournalEntries, "JournalEntries");
            RelatedEntities.Add(RelatedTransactionEntity.Items, "ItemsMovements");
            RelatedEntities.Add(RelatedTransactionEntity.CashBox, "CashBoxMovements");
            RelatedEntities.Add(RelatedTransactionEntity.Bank, "BankMovements");
            RelatedEntities.Add(RelatedTransactionEntity.Checks, "ChecksMovements");
            #endregion
        }

        public IQueryable<Transaction> GetByFilter(int companyID, TransactionsFilter filter, List<RelatedTransactionEntity> includedEntities)
        {
            IQueryable<Transaction> query = context.Transactions.Where(t => t.CompanyID == companyID).Include(t => t.JournalEntries);

            #region filters
            //BankAccountID
            query = (filter.BankAccountID.HasValue ? query.Where(t => (t.BankAccountID == filter.BankAccountID)) : query);
            //TransactionsDates
            query = (filter.TransactionDateFrom.HasValue ? query.Where(t => (t.TransactionDate >= filter.TransactionDateFrom)) : query);
            query = (filter.TransactionDateTo.HasValue ? query.Where(t => (t.TransactionDate <= filter.TransactionDateTo)) : query);
            //IssuesDates
            query = (filter.IssueDateFrom.HasValue ? query.Where(t => (t.IssueDate >= filter.IssueDateFrom)) : query);
            query = (filter.IssueDateTo.HasValue ? query.Where(t => (t.IssueDate <= filter.IssueDateTo)) : query);
            //DuesDates
            query = (filter.DueDateFrom.HasValue ? query.Where(t => (t.IssueDate >= filter.IssueDateFrom)) : query);
            query = (filter.DueDateTo.HasValue ? query.Where(t => (t.IssueDate <= filter.IssueDateTo)) : query);
            //DocumentsTypeID
            query = (filter.DocumentsTypeID.Any() ? query.Where(t => filter.DocumentsTypeID.Contains(t.DocumentTypeID)) : query);
            //InternalNumbers
            query = (filter.PrefixInternalNumberFrom.HasValue ? query.Where(t => (t.PrefixInternalNumber >= filter.PrefixInternalNumberFrom)) : query);
            query = (filter.PrefixInternalNumberTo.HasValue ? query.Where(t => (t.PrefixInternalNumber <= filter.PrefixInternalNumberTo)) : query);
            query = (filter.InternalNumberFrom.HasValue ? query.Where(t => (t.InternalNumber >= filter.PrefixInternalNumberFrom)) : query);
            query = (filter.InternalNumberTo.HasValue ? query.Where(t => (t.InternalNumber <= filter.PrefixInternalNumberTo)) : query);
            //ExternalNumber
            query = (filter.PrefixExternalNumber.HasValue ? query.Where(t => (t.PrefixExternalNumber == filter.PrefixExternalNumber)) : query);
            query = (filter.ExternalNumber.HasValue ? query.Where(t => (t.ExternalNumber == filter.ExternalNumber)) : query);
            //ExternalDates
            query = (filter.ExternalDateFrom.HasValue ? query.Where(t => (t.ExternalDate >= filter.ExternalDateFrom)) : query);
            query = (filter.ExternalDateTo.HasValue ? query.Where(t => (t.ExternalDate <= filter.ExternalDateTo)) : query);
            //PrefixEntity
            if (filter.PrefixEntity.HasValue)
            {
                query.Where(t => t.PrefixEntity == filter.PrefixEntity);
                query = (filter.EntityIdFrom.HasValue ? query.Where(t => (t.EntityId >= filter.EntityIdFrom)) : query);
                query = (filter.EntityIdTo.HasValue ? query.Where(t => (t.EntityId <= filter.EntityIdTo)) : query);
            }
            //PaymentsTypeID
            query = (filter.PaymentsTypeID.Any() ? query.Where(t => (t.PaymentTypeId != null && filter.PaymentsTypeID.Contains(t.PaymentTypeId))) : query);
            //BuySellConditionsID
            query = (filter.BuySellConditionsID.Any() ? query.Where(t => (t.BuySellConditionID != null && filter.BuySellConditionsID.Contains(t.BuySellConditionID.Value))) : query);
            //SpecializedJournalsID
            query = (filter.SpecializedJournalsID.Any() ? query.Where(t => (t.SpecializedJournalID != null && filter.SpecializedJournalsID.Contains(t.SpecializedJournalID.Value))) : query);
            //BankAccountID
            query = (filter.BankAccountID.HasValue ? query.Where(t => (t.BankAccountID == filter.BankAccountID)) : query);
            //CoinID
            query = (filter.CoinID.HasValue ? query.Where(t => (t.CoinID == filter.CoinID)) : query);
            //OriginCashBoxID
            query = (filter.OriginCashBoxID.HasValue ? query.Where(t => (t.OriginCashBoxID == filter.OriginCashBoxID)) : query);
            //DestinationCashBoxID
            query = (filter.DestinationCashBoxID.HasValue ? query.Where(t => (t.DestinationCashBoxID == filter.DestinationCashBoxID)) : query);
            //Total
            query = (filter.TotalFrom.HasValue ? query.Where(t => (t.Total >= filter.TotalFrom)) : query);
            query = (filter.TotalTo.HasValue ? query.Where(t => (t.Total <= filter.TotalTo)) : query);
            //TotalTaxable
            query = (filter.TotalTaxableFrom.HasValue ? query.Where(t => (t.TotalTaxable >= filter.TotalTaxableFrom)) : query);
            query = (filter.TotalTaxableTo.HasValue ? query.Where(t => (t.TotalTaxable <= filter.TotalTaxableTo)) : query);
            //TotalTaxes
            query = (filter.TotalTaxesFrom.HasValue ? query.Where(t => (t.TotalTaxes >= filter.TotalTaxesFrom)) : query);
            query = (filter.TotalTaxesTo.HasValue ? query.Where(t => (t.TotalTaxes <= filter.TotalTaxesTo)) : query);
            //Eliminated
            query = query.Where(t => t.Eliminated == filter.Eliminated);
            //Modified
            query = query.Where(t => t.Modified == filter.Modified);
            //Observations
            query = string.IsNullOrWhiteSpace(filter.Observations) ? query : query.Where(t => (t.Observations != null && t.Observations.Contains(filter.Observations)));
            //UserID
            query = (filter.UserID.HasValue ? query.Where(t => (t.UserID == filter.UserID)) : query);
            //IP
            query = string.IsNullOrWhiteSpace(filter.IP) ? query : query.Where(t => (t.IP.Contains(filter.IP)));
            //LastModificationFrom
            query = (filter.LastModificationFrom.HasValue ? query.Where(t => (t.LastModification >= filter.LastModificationFrom)) : query);
            query = (filter.LastModificationTo.HasValue ? query.Where(t => (t.LastModification <= filter.LastModificationTo)) : query);
            //ClosingJournalEntry
            query = (filter.ClosingJournalEntry.HasValue ? query.Where(t => (t.ClosingJournalEntry == filter.ClosingJournalEntry.Value)) : query);
            //ClosingJournal
            query = (filter.ClosingJournal.HasValue ? query.Where(t => (t.ClosingJournal == filter.ClosingJournal.Value)) : query);
            //ClosingJournal
            query = (filter.ProfitLossesClose.HasValue ? query.Where(t => (t.ProfitLossesClose == filter.ProfitLossesClose.Value)) : query);
            #endregion
            //Include related entities
            if (includedEntities.Any())
            {
                includedEntities.ForEach(e => query.Include(RelatedEntities[e]));
            }
            return query;
        }

        public async Task<List<TransactionDTO>> GetByFilterAsync(int companyID, TransactionsFilter filter, List<RelatedTransactionEntity>? relatedTransactionEntities = null)
        {
            relatedTransactionEntities = relatedTransactionEntities ?? new List<RelatedTransactionEntity>();
            IQueryable<Transaction> query = this.GetByFilter(companyID, filter, relatedTransactionEntities);
            List<TransactionDTO> retValue = (await query.ToListAsync()).Select(t => Mappers.DtoMappers.MapTransactionToDTO(t)).ToList();
            return retValue;
        }

        public async Task<TransactionDTO?> GetByIDAsync(long transactionID, List<RelatedTransactionEntity>? relatedTransactionEntities = null)
        {
            IQueryable<Transaction> query = (from t in context.Transactions
                                             where t.TransactionID == transactionID
                                             select t);
            relatedTransactionEntities?.ForEach(e => query.Include(RelatedEntities[e]));
            Transaction? transaction = await query.FirstOrDefaultAsync();
            return (transaction == null ? null : Mappers.DtoMappers.MapTransactionToDTO(transaction));
        }

        public async Task<long> AddAsync(TransactionDTO transactionDTO)
        {
            try
            {
                Transaction transaction = Mappers.EntityMappers.MapToTransaction(transactionDTO);
                transaction.TransactionDate = currentDate;
                context.Transactions.Add(transaction);
                await context.SaveChangesAsync();
                return transaction.TransactionID;
            }
            catch {
                throw;
            }
        }

        public async Task UpdateAsync(TransactionDTO transactionDTO, AuditTransactionDTO auditTransactionDTO)
        {
            try
            {
                Transaction transaction = Mappers.EntityMappers.MapToTransaction(transactionDTO);
                context.Transactions.Update(transaction);

                AuditTransaction auditTransaction = Mappers.EntityMappers.MapToAuditTransaction(auditTransactionDTO);
                context.AuditTransactions.Add(auditTransaction);

                //Saves all the changes and return the new TransactionID
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteAsync(long transactionID, AuditTransactionDTO auditTransactionDTO)
        {
            try
            {
                //Verify transaction exists and isn't elminated
                Transaction? transaction = await (from t in context.Transactions
                                                  where t.TransactionID == transactionID && t.Eliminated == false
                                                  select t).FirstOrDefaultAsync();
                if (transaction == null)
                {
                    throw new NullReferenceException("Transaction doesn't exists, please verify.");
                }
                //Mark original transation as deleted
                AuditTransaction auditTransaction = Mappers.EntityMappers.MapToAuditTransaction(auditTransactionDTO);
                transaction.Eliminated = true;
                context.Transactions.Update(transaction);
                //Generates a new audit record
                context.AuditTransactions.Add(auditTransaction);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
