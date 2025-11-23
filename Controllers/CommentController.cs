using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Constants;
using backend.Dtos.CommentDtoNamespace;
using backend.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using backend.Models;

namespace backend.Controllers
{   
    [Route("[controller]")]
    [ApiController]
    public class CommentController: ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly UserManager<DefaultUser> _userManager;
        public CommentController(ICommentRepository commentRepository, UserManager<DefaultUser> userManager){
            _commentRepository = commentRepository;
            _userManager = userManager;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateComment(CreateCommentDto createCommentDto){
            var user = await _userManager.GetUserAsync(User);
            if (user == null){
                return Unauthorized("User not found");
            }
            var (comment, statusCode) = await _commentRepository.CreateComment(createCommentDto.ToModel(), user);
            if (statusCode == StatusCodeConstants.STOCK_NOT_FOUND){
                return NotFound("Stock not found");
            }
            if (comment == null){
                return BadRequest("Failed to create comment");
            }
            return Ok(comment.ToDto());
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment([FromRoute] int id, [FromBody] CreateCommentDto createCommentDto){
            var (comment, statusCode) = await _commentRepository.UpdateComment(id, createCommentDto);
            if (statusCode == StatusCodeConstants.COMMENT_NOT_FOUND){
                return NotFound("Comment not found");
            }
            if (comment == null){
                return BadRequest("Failed to update comment");
            }
            return Ok(comment.ToDto());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id){
            var (comment, statusCode) = await _commentRepository.DeleteComment(id);
            if (statusCode == StatusCodeConstants.COMMENT_NOT_FOUND){
                return NotFound("Comment not found");
            }
            if (comment == null){
                return BadRequest("Failed to delete comment");
            }
            return Ok(comment.ToDto());
        }
    }
}