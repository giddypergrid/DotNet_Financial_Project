using backend.Models;
using backend.Dtos.CompanyStockDtoNamespace;
namespace backend.Repository.Interface
{
    public interface IStockRepository
    {
        Task<List<CompanyStock>> GetAllStocks();
        Task<CompanyStock> GetStockById(int id);
        Task<CompanyStock> GetStockBySymbol(string symbol);
        Task<CompanyStock> CreateStock(CompanyStock companyStock);
        Task<CompanyStock> UpdateStock(int id, CreateCompanyStockDto createCompanyStockDto);
        Task<CompanyStock> DeleteStock(int id);

    }
}