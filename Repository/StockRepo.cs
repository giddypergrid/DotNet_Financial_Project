using backend.Repository.Interface;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Dtos.CompanyStockDtoNamespace;
using backend.Constants;
using backend.Helpers.Objects;

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
        public async Task<(List<CompanyStock>, int)> SearchStocks(queryStockObject queryStockObject){
            var queryable = _dbContext.CompanyStocks.AsQueryable();
            bool hasSymbol = !string.IsNullOrEmpty(queryStockObject.Symbol);
            bool hasCompanyName = !string.IsNullOrEmpty(queryStockObject.CompanyName);
            int pageIndex = queryStockObject.PageIndex ?? 1;
            int pageSize = queryStockObject.PageSize ?? 10;
            if (hasSymbol){
                queryable = queryable.Where(s => s.Symbol.Contains(queryStockObject.Symbol));
                if(queryStockObject.isDescending){
                    queryable = queryable.OrderByDescending(s => s.Symbol);
                }else{
                    queryable = queryable.OrderBy(s => s.Symbol);
                }
            }
            if (hasCompanyName){
                queryable = queryable.Where(s => s.CompanyName.Contains(queryStockObject.CompanyName));
                if(queryStockObject.isDescending){
                    queryable = queryable.OrderByDescending(s => s.CompanyName);
                }else{
                    queryable = queryable.OrderBy(s => s.CompanyName);
                }
            }
            if(!hasSymbol && !hasCompanyName){
                return (new List<CompanyStock>([]), 0);
            }
            int totalCount = await queryable.CountAsync();
            bool hasCursor = !string.IsNullOrEmpty(queryStockObject.LastStringId);
            if(hasCursor){
                if(hasSymbol){
                    if (queryStockObject.isDescending){
                        queryable = queryable.Where(s => s.Symbol.CompareTo(queryStockObject.LastStringId) < 0);
                    }else{
                        queryable = queryable.Where(s => s.Symbol.CompareTo(queryStockObject.LastStringId) > 0);
                    }
                }
                if(hasCompanyName){
                    if (queryStockObject.isDescending){
                        queryable = queryable.Where(s => s.CompanyName.CompareTo(queryStockObject.LastStringId) < 0);
                    }else{
                        queryable = queryable.Where(s => s.CompanyName.CompareTo(queryStockObject.LastStringId) > 0);
                    }
                }
            }else{
                queryable = queryable.Skip((pageIndex - 1) * pageSize);
            }
            var stocks = await queryable.Take(pageSize + 1).ToListAsync();
            return (stocks, totalCount);
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
        public async Task<(CompanyStock?, int)> CreateStock(CreateCompanyStockDto createCompanyStockDto)
        {
                        
            var existingStock = await _dbContext.CompanyStocks.FirstOrDefaultAsync(s => s.Symbol == createCompanyStockDto.Symbol);
            if (existingStock != null)
            {
                return (null, StatusCodeConstants.COMMENT_EXIST_WHEN_CREATE);

            }

            var companyStock = createCompanyStockDto.ToModel();
            await _dbContext.CompanyStocks.AddAsync(companyStock);
            await _dbContext.SaveChangesAsync();
            return (companyStock, StatusCodeConstants.SUCCESS);
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

            await _dbContext.SaveChangesAsync();
            return existingStock;
        }
        public async Task<bool> isStockExist(int stockId, string symbol = "")
        {
            if (!string.IsNullOrEmpty(symbol))
            {
                return await _dbContext.CompanyStocks.AnyAsync(s => s.Symbol == symbol);
            }
            return await _dbContext.CompanyStocks.AnyAsync(s => s.Id == stockId);
        }
    }
}