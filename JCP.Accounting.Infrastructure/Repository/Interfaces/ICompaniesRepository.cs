
using Accounting.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Infrastructure.Interfaces
{
    public interface ICompaniesRepository
    {
        Task<List<Company>> GetAllAsync();
        Task<Company?> GetByIdAsync(int companyID);
        Task<List<Company>> GetByFilterAsync(string name);
        Task<int> UpdateAsync(Company company);
        Task<int> DeleteAsync(int companyID);
        Task AddAsync(Company company);
    }
}
