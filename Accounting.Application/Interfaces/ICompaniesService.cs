using Accounting.Domain.DTO;

namespace Accounting.Application.Interfaces
{
    public interface ICompaniesService : IDisposable
    {
        Task<List<CompanyDTO>> GetAllAsync();
        Task<CompanyDTO?> GetByIdAsync(int companyID);
        Task<List<CompanyDTO>> GetByFilterAsync(string name);
        Task<int> AddAsync(CompanyDTO companyDTO);
        Task UpdateAsync(CompanyDTO companyDTO);
        Task DeleteAsync(int companyID);
    }
}
