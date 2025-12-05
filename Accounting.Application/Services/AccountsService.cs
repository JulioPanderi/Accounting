using Accounting.Application.Interfaces;
using Accounting.Domain.DTO;
using Accounting.Infrastructure.Models;
using Accounting.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Accounting.Application.Services
{
    public class AccountsService : IAccountsService
    {
        private readonly IAccountsRepository accountsRepository;
        private readonly IServiceScope scope;
        private readonly ILogger<AccountsService> logger;
        private bool disposedValue;

        public AccountsService(IAccountsRepository accountsRepository, IServiceScopeFactory serviceScopeFactory, ILogger<AccountsService> logger) 
        {
            this.accountsRepository = accountsRepository;
            scope = serviceScopeFactory.CreateScope();
            this.logger = logger;
        }

        public async Task<List<AccountDTO>>GetAllAsync(int companyID)
        {
            List<AccountDTO> retValue = new List<AccountDTO>();
            var accounts = await accountsRepository.GetAllAsync(companyID);
            if (accounts!= null)
            {
                retValue = (from a in accounts select MapToDTO(a)).ToList();
            }
            return retValue;
        }

        public async Task<AccountDTO?> GetByIdAsync(int companyID, string accountID)
        {
            AccountDTO? retValue = null;
            var account = await accountsRepository.GetByIDAsync(companyID, accountID);
            if (account != null)
            {
                retValue = MapToDTO(account);
            }
            return retValue;
        }

        public async Task<int> UpdateAsync(AccountDTO accountDTO)
        {
            Account account = Map(accountDTO);
            return await accountsRepository.UpdateAsync(account);
        }

        public async Task<int> DeleteAsync(int companyID, string accountID)
        {
            return await accountsRepository.DeleteAsync(companyID, accountID);
        }

        public async Task AddAsync(AccountDTO accountDTO)
        {
            Account account = Map(accountDTO);
            await accountsRepository.AddAsync(account);
        }

        internal static Account Map(AccountDTO accountDTO)
        {
            Account retValue = new Account()
            {
                AccountID = accountDTO.AccountID,
                CoinID = accountDTO.CoinID,
                CompanyID = accountDTO.CompanyID,
                Description = accountDTO.Description,
                IsResultAccount = accountDTO.IsResultAccount,
                IsClientAccount = accountDTO.IsClientAccount,
                IsProviderAccount = accountDTO.IsProviderAccount,
                IsCashAccount = accountDTO.IsCashAccount,
                IsBankAccount = accountDTO.IsBankAccount
            };
            return retValue;
        }

        internal static AccountDTO MapToDTO(Account account)
        {
            AccountDTO retValue = new AccountDTO()
            {
                AccountID = account.AccountID,
                CoinID = account.CoinID,
                CompanyID = account.CompanyID,
                Description = account.Description,
                IsResultAccount = account.IsResultAccount,
                Coin = (account.Coin == null ? null : CoinsService.MapToDTO(account.Coin))
            };
            return retValue;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing) scope.Dispose();
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
   
}
