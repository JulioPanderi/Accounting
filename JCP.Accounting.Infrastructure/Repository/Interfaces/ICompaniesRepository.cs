using Accounting.Domain.DTO;

namespace Accounting.Infrastructure.Interfaces
{
    public interface ICompaniesRepository
    {
        Task<List<CompanyDTO>> GetAllAsync();
        Task<CompanyDTO?> GetByIdAsync(int companyID);
        Task<List<CompanyDTO>> GetByFilterAsync(string name);
        Task UpdateAsync(CompanyDTO companyDTO);
        Task DeleteAsync(int companyID);
        Task<int> AddAsync(CompanyDTO companyDTO);
    }
}
