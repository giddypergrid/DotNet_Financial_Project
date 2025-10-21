using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.CompanyStockDtoNamespace;
using backend.Models;
using Mapster;  

namespace backend.Dtos.CommentDtoNamespace
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Content { get; set; } = String.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int CompanyStockId { get; set; }
    }
    public static class CommentDtoMapper
    {
        public static CommentDto ToDto(this Comment comment, bool includeCompanyStock = true)
        {
           CommentDto commentDto =  comment.Adapt<CommentDto>();
           return commentDto;
        }
        public static Comment ToModel(this CommentDto commentDto, bool includeCompanyStock = true)
        {
            Comment comment = commentDto.Adapt<Comment>();
            return comment;
        }
    }
}