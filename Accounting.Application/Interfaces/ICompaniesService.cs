using Accounting.Domain.DTO;
using Accounting.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Application.Interfaces
{
    public interface ICompaniesService : IDisposable
    {
        Task<List<CompanyDTO>> GetAllAsync();
        Task<CompanyDTO?> GetByIdAsync(int companyID);
        Task<List<CompanyDTO>> GetByFilterAsync(string name);
        Task<int> UpdateAsync(CompanyDTO companyDTO);
        Task<int> DeleteAsync(int companyID);
        Task AddAsync(CompanyDTO companyDTO);
    }
}
