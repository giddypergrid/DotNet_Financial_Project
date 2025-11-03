using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Constants;
using backend.Data;
using backend.Models;
using backend.Repository.Interface;
using backend.Dtos.CommentDtoNamespace;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class CommentRepo: ICommentRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public CommentRepo(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> isStockExist(int stockId)
        {
            return await _dbContext.CompanyStocks.AnyAsync(s => s.Id == stockId);
        }
        public async Task<bool> isCommentExist(int commentId){
            return await _dbContext.Comments.AnyAsync(c => c.Id == commentId);
        }
        public async Task<Comment?> GetCommentById(int id)
        {
            return await _dbContext.Comments.FindAsync(id);
        }
        public async Task<(Comment?, int)> CreateComment(Comment comment)
        {
            if (await isStockExist(comment.CompanyStockId) == false)
            {
                return (null, StatusCodeConstants.STOCK_NOT_FOUND);
            }
            
            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
            return (comment, StatusCodeConstants.SUCCESS);
        }
        public async Task<(Comment?, int)> UpdateComment(int id, CreateCommentDto createCommentDto)
        {
            var existingComment = await _dbContext.Comments.FindAsync(id);
            if (existingComment==null)
            {
                return (null, StatusCodeConstants.COMMENT_NOT_FOUND);
            }
            existingComment.Title = createCommentDto.Title;
            existingComment.Content = createCommentDto.Content;
            existingComment.CompanyStockId = createCommentDto.CompanyStockId;
            existingComment.UpdatedOn = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
            return (existingComment, StatusCodeConstants.SUCCESS);
        }
        public async Task<(Comment?, int)> DeleteComment(int id)
        {
            var existingComment = await _dbContext.Comments.FindAsync(id);
            if (existingComment == null)
            {
                return (null, StatusCodeConstants.COMMENT_NOT_FOUND);
            }
            _dbContext.Comments.Remove(existingComment);
            await _dbContext.SaveChangesAsync();
            return (existingComment, StatusCodeConstants.SUCCESS);
        }
    }
}