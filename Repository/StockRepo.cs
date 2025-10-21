using backend.Repository.Interface;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Dtos.CompanyStockDtoNamespace;

namespace backend.Repository
{
    public class StockRepo : IStockRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public StockRepo(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<CompanyStock>> GetAllStocks()
        {
            return await _dbContext.CompanyStocks.ToListAsync();
        }
        public async Task<CompanyStock> GetStockBySymbol(string symbol)
        {
            var stock = await _dbContext.CompanyStocks.FirstOrDefaultAsync(s => s.Symbol == symbol);
            return stock;
        }
        public async Task<CompanyStock> GetStockById(int id)
        {
            var stock = await _dbContext.CompanyStocks.FindAsync(id);
            return stock;
        }
        public async Task<CompanyStock> CreateStock(CompanyStock companyStock)
        {
            await _dbContext.CompanyStocks.AddAsync(companyStock);
            await _dbContext.SaveChangesAsync();
            return companyStock;
        }
        public async Task<CompanyStock> DeleteStock(int id)
        {
            var stock = await _dbContext.CompanyStocks.FindAsync(id);
            if (stock == null) return null;
            _dbContext.CompanyStocks.Remove(stock);
            await _dbContext.SaveChangesAsync();
            return stock;
        }
        public async Task<CompanyStock> UpdateStock(int id, CreateCompanyStockDto updateCompanyStockDto)
        {
            var existingStock = await _dbContext.CompanyStocks.FindAsync(id);
            if (existingStock == null) return null;

            existingStock.CompanyName = updateCompanyStockDto.CompanyName;
            existingStock.Symbol = updateCompanyStockDto.Symbol;
            existingStock.Purchase = updateCompanyStockDto.Purchase;
            existingStock.LastDiv = updateCompanyStockDto.LastDiv;
            existingStock.Industry = updateCompanyStockDto.Industry;
            existingStock.MarketCap = updateCompanyStockDto.MarketCap;
            existingStock.CommentIds = updateCompanyStockDto.CommentIds;

            await _dbContext.SaveChangesAsync();
            return existingStock;
        }
    }
}