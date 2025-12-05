using Accounting.Infrastructure.Filters;
using Accounting.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Accounting.Infrastructure.Repository
{
    internal enum RelatedTransactionEntries
    {
        JournalEntries,
        CashBox,
        Bank,
        Checks
    }
    internal class TransactionRespositoryHelper
    {
        private readonly AccountingDbContext context;
        public TransactionRespositoryHelper(AccountingDbContext context)
        {
            this.context = context;
        }

        internal IQueryable<Transaction> GetByFilter(int companyID, TransactionsFilter filter, List<RelatedTransactionEntries>? includedEntries = null)
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
            if(includedEntries != null)
            {
                foreach (RelatedTransactionEntries entry in includedEntries)
                {
                    switch (entry)
                    {
                        //TODO: as new related entities funtionalities implemented, add the correponding include
                        case RelatedTransactionEntries.JournalEntries:
                            query.Include(t => t.JournalEntries);
                            break;
                    }
                }
            }
            return query;
        }
    }
}
