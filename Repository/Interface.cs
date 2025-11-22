using backend.Models;
using backend.Dtos.CompanyStockDtoNamespace;
using backend.Dtos.CommentDtoNamespace;
using backend.Helpers;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace backend.Repository.Interface
{
    public interface IStockRepository{
        Task<(List<CompanyStock>, int)> SearchStocks(queryStockObject queryStockObject);
        Task<CompanyStock> GetStockById(int id);
        Task<(CompanyStock?, int)> CreateStock(CreateCompanyStockDto createCompanyStockDto);
        Task<CompanyStock> UpdateStock(int id, CreateCompanyStockDto createCompanyStockDto);
        Task<CompanyStock> DeleteStock(int id);
        Task<bool> isStockExist(int stockId, string symbol = "");

    }

    public interface ICommentRepository{
        Task<Comment?> GetCommentById(int id);
        Task<(Comment?, int)> CreateComment(Comment comment);
        Task<(Comment?, int)> UpdateComment(int id, CreateCommentDto createCommentDto);
        Task<(Comment?, int)> DeleteComment(int id);
        Task<bool> isStockExist(int stockId);
        Task<bool> isCommentExist(int commentId);

    }
    public interface IUserRepository{
        Task<DefaultUser?> isUserExist(ClaimsPrincipal user, UserManager<DefaultUser> userManager);
        Task<List<CompanyStock>> GetUserStocks(DefaultUser user);
        Task<List<CompanyStock>> AddUserStock(List<string> symbols, DefaultUser user);
        Task<bool?> DeleteUserStock(List<int> stockIds, DefaultUser user);
    }
}