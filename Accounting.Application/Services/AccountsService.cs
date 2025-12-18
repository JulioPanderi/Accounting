using Accounting.Application.Interfaces;
using Accounting.Domain.DTO;
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
            List<AccountDTO> retValue = await accountsRepository.GetAllAsync(companyID);
            return retValue;
        }

        public async Task<AccountDTO?> GetByIdAsync(int companyID, string accountID)
        {
            AccountDTO? retValue = await accountsRepository.GetByIDAsync(companyID, accountID);
            return retValue;
        }

        public async Task UpdateAsync(AccountDTO accountDTO)
        {
            await accountsRepository.UpdateAsync(accountDTO);
        }

        public async Task DeleteAsync(int companyID, string accountID)
        {
            await accountsRepository.DeleteAsync(companyID, accountID);
        }

        public async Task AddAsync(AccountDTO accountDTO)
        {
            await accountsRepository.AddAsync(accountDTO);
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
