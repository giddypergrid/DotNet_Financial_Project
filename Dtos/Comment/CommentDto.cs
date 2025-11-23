using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.CompanyStockDtoNamespace;
using backend.Dtos.User;
using backend.Models;
using Mapster;  

namespace backend.Dtos.CommentDtoNamespace
{
    public class CreateCommentDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string Title { get; set; } = String.Empty;
        
        [Required(ErrorMessage = "Content is required")]
        [StringLength(500, ErrorMessage = "Content cannot exceed 500 characters")]
        public string Content { get; set; } = String.Empty;
        
        [Required(ErrorMessage = "CompanyStockId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "CompanyStockId must be a positive number")]
        public int CompanyStockId { get; set; }
    }
    public class CommentDto: CreateCommentDto
    {
        public UserDisplayDto User { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
    
    public static class CommentDtoMapper
    {
        public static CommentDto ToDto(this Comment comment)
        {
           CommentDto commentDto =  comment.Adapt<CommentDto>();
           commentDto.User = comment.User.ToDto();
           return commentDto;
        }
        public static Comment ToModel(this CreateCommentDto createCommentDto){
            Comment comment = createCommentDto.Adapt<Comment>();
            return comment;
        }
    }
}