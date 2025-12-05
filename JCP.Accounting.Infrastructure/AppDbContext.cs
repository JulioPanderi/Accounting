using Accounting.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Accounting.Infrastructure
{
    public class AccountingDbContext : DbContext
    {
        #region DbContext

        public AccountingDbContext() { }

        public AccountingDbContext(DbContextOptions<AccountingDbContext> options)
            : base(options)
        {
        }

        #endregion

        #region Entities
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Coin> Coins { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<JournalEntry> JournalEntries { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        #endregion
    }
}
