using Accounting.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Services
{
    public class LedgerBookService
    {
        private readonly ILedgerBookRepository ledgerBookRepository;
        private readonly IServiceScope scope;
        private readonly ILogger<CoinsService> logger;
        private bool disposedValue;

        public LedgerBookService(ILedgerBookRepository ledgerBookRepository, IServiceScopeFactory serviceScopeFactory, ILogger<CoinsService> logger)
        {
            this.ledgerBookRepository = ledgerBookRepository;
            scope = serviceScopeFactory.CreateScope();
            this.logger = logger;
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
