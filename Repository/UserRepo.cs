using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Repository.Interface;
using backend.Data;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using backend.Extensions;
using Microsoft.AspNetCore.Identity;
using backend.Dtos.CompanyStockDtoNamespace;
namespace backend.Repository
{
    public class UserRepo: IUserRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public UserRepo(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<DefaultUser?> GetDefaultUser(ClaimsPrincipal user, UserManager<DefaultUser> userManager)
        {
            string userId = user.getClaimUserId();
            if (userId == null){
                return null;
            }
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<List<CompanyStock>> AddUserStock(List<string> symbols, DefaultUser user)
        {
            List<CompanyStock> stocks = await _dbContext.CompanyStocks
                .Where(c => symbols.Contains(c.Symbol))
                .AsNoTracking()
                .OrderBy(c => c.Symbol)
                .ToListAsync();
            foreach (CompanyStock stock in stocks){
                _dbContext.Portfolios.Add(
                    new Portfolio{
                        DefaultUserId = user.Id,
                        CompanyStockId = stock.Id
                    }
                );
            }
            await _dbContext.SaveChangesAsync();
            return stocks;
        }

        public async Task<List<CompanyStock>> GetUserStocks(DefaultUser user)
        {
            return await _dbContext.Portfolios
                .Where(p => p.DefaultUserId == user.Id)
                .Select(p => p.CompanyStock)
                .ToListAsync();
        }

        public async Task<bool?> DeleteUserStock(List<int> stockIds, DefaultUser user){
            foreach (int stockId in stockIds){
                var stock = await _dbContext.CompanyStocks.FindAsync(stockId);
                if (stock == null){
                    return null;
                }
                _dbContext.Portfolios.Remove(new Portfolio{
                    DefaultUserId = user.Id,
                    CompanyStockId = stock.Id
                });
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}