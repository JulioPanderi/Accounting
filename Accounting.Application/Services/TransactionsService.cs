using Accounting.Domain.DTO;
using Accounting.Domain.Filters;
using Accounting.Infrastructure.Filters;
using Accounting.Infrastructure.Interfaces;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Services
{
    internal class TransactionsService
    {
        private readonly ITransactionsRepository transactionsRepository;
        public async Task<List<TransactionDTO>> GetByFiltersAsync(int companyID, TransactionsFilterDTO filterDTO)
        {
            TransactionsFilter filter = MapFilter(filterDTO);
            var transactions = await transactionsRepository.GetByFiltersAsync(companyID, filter);
            List<TransactionDTO> retValue = transactions.Select(t => MapToDTO(t)).ToList();
            return retValue;
        }

        public async Task<TransactionDTO?> GetByIDAsync(long transactionID)
        {
            var transaction = await transactionsRepository.GetByIDAsync(transactionID);
            TransactionDTO retValue = (transaction!= null) ? MapToDTO(transaction): new TransactionDTO();
            return retValue;
        }

        public async Task<long> AddAsync(TransactionDTO transactionDTO)
        {
            try
            {
                Transaction transaction = Map(transactionDTO);
                long retValue = await transactionsRepository.AddAsync(transaction);
                return retValue;
            }
            catch
            {
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

        public async Task<int> DeleteAsync(TransactionDTO transactionDTO)
        {
            //TODO: implement delete, recording auditory registry and mark transaction as eliminated
            throw new InvalidOperationException("Transaction delete is not allowed.");

            //context.Transactions.Update(transaction);
            //return await context.SaveChangesAsync();
        }

        internal TransactionsFilter MapFilter(TransactionsFilterDTO transactionsFilterDTO)
        {
            TransactionsFilter transactionsFilter = new TransactionsFilter()
            {
                TransactionID = transactionsFilterDTO.TransactionID,
                TransactionDateFrom = transactionsFilterDTO.TransactionDateFrom,
                TransactionDateTo = transactionsFilterDTO.TransactionDateTo,
                IssueDateFrom = transactionsFilterDTO.IssueDateFrom,
                IssueDateTo = transactionsFilterDTO.IssueDateTo,
                DueDateFrom = transactionsFilterDTO.DueDateFrom,
                DueDateTo = transactionsFilterDTO.DueDateTo,
                DocumentsTypeID = transactionsFilterDTO.DocumentsTypeID,
                PrefixInternalNumberFrom = transactionsFilterDTO.PrefixInternalNumberFrom,
                PrefixInternalNumberTo = transactionsFilterDTO.PrefixInternalNumberTo,
                InternalNumberFrom = transactionsFilterDTO.InternalNumberFrom,
                InternalNumberTo = transactionsFilterDTO.InternalNumberTo,
                PrefixExternalNumber = transactionsFilterDTO.PrefixExternalNumber,
                ExternalNumber = transactionsFilterDTO.ExternalNumber,
                ExternalDateFrom = transactionsFilterDTO.ExternalDateFrom,
                ExternalDateTo = transactionsFilterDTO.ExternalDateTo,
                PrefixEntity = transactionsFilterDTO.PrefixEntity,
                EntityIdFrom = transactionsFilterDTO.EntityIdFrom,
                EntityIdTo = transactionsFilterDTO.EntityIdTo,
                PaymentsTypeID = transactionsFilterDTO.PaymentsTypeID,
                BuySellConditionsID = transactionsFilterDTO.BuySellConditionsID,
                SpecializedJournalsID = transactionsFilterDTO.SpecializedJournalsID,
                BankAccountID = transactionsFilterDTO.BankAccountID,
                CoinID = transactionsFilterDTO.CoinID,
                OriginCashBoxID = transactionsFilterDTO.OriginCashBoxID,
                DestinationCashBoxID = transactionsFilterDTO.DestinationCashBoxID,
                TotalFrom = transactionsFilterDTO.TotalFrom,
                TotalTo = transactionsFilterDTO.TotalTo,
                TotalTaxableFrom = transactionsFilterDTO.TotalTaxableFrom,
                TotalTaxableTo = transactionsFilterDTO.TotalTaxableTo,
                TotalTaxesFrom = transactionsFilterDTO.TotalTaxesFrom,
                TotalTaxesTo = transactionsFilterDTO.TotalTaxesTo,
                Eliminated = transactionsFilterDTO.Eliminated,
                Modified = transactionsFilterDTO.Modified,
                Observations = transactionsFilterDTO.Observations,
                UserID = transactionsFilterDTO.UserID,
                IP = transactionsFilterDTO.IP,
                LastModificationFrom = transactionsFilterDTO.LastModificationFrom,
                LastModificationTo = transactionsFilterDTO.LastModificationTo,
                ClosingJournalEntry = transactionsFilterDTO.ClosingJournalEntry,
                ClosingJournal = transactionsFilterDTO.ClosingJournal,
                ProfitLossesClose = transactionsFilterDTO.ProfitLossesClose
            };
            return transactionsFilter;
        }

        internal TransactionDTO MapToDTO(Transaction transaction)
        {
            TransactionDTO retValue = new TransactionDTO()
            {
                TransactionID = transaction.TransactionID,
                CompanyID = transaction.CompanyID,
                TransactionDate = transaction.TransactionDate,
                IssueDate = transaction.IssueDate,
                DueDate = transaction.DueDate,
                DocumentTypeID = transaction.DocumentTypeID,
                PrefixInternalNumber = transaction.PrefixInternalNumber,
                InternalNumber = transaction.InternalNumber,
                PrefixExternalNumber = transaction.PrefixExternalNumber,
                ExternalNumber = transaction.ExternalNumber,
                ExternalDate = transaction.ExternalDate,
                PrefixEntity = transaction.PrefixEntity,
                EntityId = transaction.EntityId,
                PaymentTypeId = transaction.PaymentTypeId,
                BuySellConditionID = transaction.BuySellConditionID,
                TaxId = transaction.TaxId,
                SpecializedJournalID = transaction.SpecializedJournalID,
                DocBookId = transaction.DocBookId,
                BankAccountID = transaction.BankAccountID,
                CoinID = transaction.CoinID,
                OriginCashBoxID = transaction.OriginCashBoxID,
                DestinationCashBoxID = transaction.DestinationCashBoxID,
                Total = transaction.Total,
                TotalTaxable = transaction.TotalTaxable,
                TotalTaxes = transaction.TotalTaxes,
                Eliminated = transaction.Eliminated,
                Modified = transaction.Modified,
                Observations = transaction.Observations,
                UserID = transaction.UserID,
                IP = transaction.IP,
                LastModification = transaction.LastModification,
                Quote = transaction.Quote,
                ClosingJournalEntry = transaction.ClosingJournalEntry,
                ClosingJournal = transaction.ClosingJournal,
                ProfitLossesClose = transaction.ProfitLossesClose
            };
            return retValue;
        }

        internal Transaction Map(TransactionDTO transactionDTO)
        {
            Transaction retValue = new Transaction()
            {
                TransactionID = transactionDTO.TransactionID,
                CompanyID = transactionDTO.CompanyID,
                TransactionDate = transactionDTO.TransactionDate,
                IssueDate = transactionDTO.IssueDate,
                DueDate = transactionDTO.DueDate,
                DocumentTypeID = transactionDTO.DocumentTypeID,
                PrefixInternalNumber = transactionDTO.PrefixInternalNumber,
                InternalNumber = transactionDTO.InternalNumber,
                PrefixExternalNumber = transactionDTO.PrefixExternalNumber,
                ExternalNumber = transactionDTO.ExternalNumber,
                ExternalDate = transactionDTO.ExternalDate,
                PrefixEntity = transactionDTO.PrefixEntity,
                EntityId = transactionDTO.EntityId,
                PaymentTypeId = transactionDTO.PaymentTypeId,
                BuySellConditionID = transactionDTO.BuySellConditionID,
                TaxId = transactionDTO.TaxId,
                SpecializedJournalID = transactionDTO.SpecializedJournalID,
                DocBookId = transactionDTO.DocBookId,
                BankAccountID = transactionDTO.BankAccountID,
                CoinID = transactionDTO.CoinID,
                OriginCashBoxID = transactionDTO.OriginCashBoxID,
                DestinationCashBoxID = transactionDTO.DestinationCashBoxID,
                Total = transactionDTO.Total,
                TotalTaxable = transactionDTO.TotalTaxable,
                TotalTaxes = transactionDTO.TotalTaxes,
                Eliminated = transactionDTO.Eliminated,
                Modified = transactionDTO.Modified,
                Observations = transactionDTO.Observations,
                UserID = transactionDTO.UserID,
                IP = transactionDTO.IP,
                LastModification = transactionDTO.LastModification,
                Quote = transactionDTO.Quote,
                ClosingJournalEntry = transactionDTO.ClosingJournalEntry,
                ClosingJournal = transactionDTO.ClosingJournal,
                ProfitLossesClose = transactionDTO.ProfitLossesClose
            };
            return retValue;
        }
    }
}
