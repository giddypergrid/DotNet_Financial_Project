using backend.Models;
using backend.Dtos.CompanyStockDtoNamespace;
using backend.Dtos.CommentDtoNamespace;
using backend.Helpers;
    
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
}