using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using backend.Models;
using backend.Dtos.User;
using backend.Service;
using backend.Extensions;
using backend.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using backend.Dtos.CompanyStockDtoNamespace;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {
        private readonly UserManager<DefaultUser> _userManager;
        private readonly SignInManager<DefaultUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IStockRepository _stockRepository;
        private readonly IUserRepository _userRepository;
        public UserController(UserManager<DefaultUser> userManager, SignInManager<DefaultUser> signInManager, ITokenService tokenService, IStockRepository stockRepository, IUserRepository userRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _stockRepository = stockRepository;
            _userRepository = userRepository;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var user = new DefaultUser { Email = registerDto.Email, UserName = registerDto.Email };
            try{
                var createResult = await _userManager.CreateAsync(user, registerDto.Password);
                if (createResult.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new RegisterResponseDto{Email = user.Email, Role = "User", Token = _tokenService.GenerateToken(user)});
                    }else{
                        return BadRequest("falied to add role:" + roleResult.ToString());
                    }
                }else{
                    return BadRequest("falied to create user:" + createResult.ToString());
                }
            }catch(Exception e){
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || user.Email == null)
            {
                return Unauthorized("Email not found");
            }
            var checkResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!checkResult.Succeeded){
                return Unauthorized("Invalid password");
            }else{
                var userRoles = await _userManager.GetRolesAsync(user);
                return Ok(new LoginResponseDto{Email = user.Email, Role = userRoles.FirstOrDefault() ?? "User", Token = _tokenService.GenerateToken(user)});
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    user = await _userManager.FindByEmailAsync(id);
                }
                
                if (user == null)
                {
                    return NotFound("User not found");
                }
                
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Any())
                {
                    var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
                    if (!removeRolesResult.Succeeded)
                    {
                        return BadRequest("Failed to remove roles: " + removeRolesResult.ToString());
                    }
                }
                var deleteResult = await _userManager.DeleteAsync(user);
                if (deleteResult.Succeeded)
                {
                    return Ok($"User {user.Email} and their roles have been successfully deleted");
                }
                else
                {
                    return BadRequest("Failed to delete user: " + deleteResult.ToString());
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getUserStocks")]
        [Authorize]
        public async Task<IActionResult> GetUserStocks()
        {
            var user = await _userRepository.GetDefaultUser(User, _userManager);
            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok((await _userRepository.GetUserStocks(user)).Select(stock=>stock.ToDtoNoComments()));
        }

        [HttpPost("addUserStocks")]
        [Authorize]
        public async Task<IActionResult> AddUserStocks([FromBody] List<string> symbols)
        {
            var user = await _userRepository.GetDefaultUser(User, _userManager);
            if (user == null)
            {
                return Unauthorized("User not found");
            }
            if (symbols.Count == 0)
            {
                return BadRequest("Symbols list is empty");
            }
            return Ok((await _userRepository
                        .AddUserStock(symbols.Select(symbol=>symbol.Trim().ToUpper()).ToList(), user))
                        .Select(stock=>stock.ToDtoNoComments()
                    ));
        }
        [HttpDelete("deleteUserStocks")]
        [Authorize]
        public async Task<IActionResult> DeleteUserStock([FromBody] List<int> stockIds){
            var user = await _userRepository.GetDefaultUser(User, _userManager);
            if (user == null)
            {
                return Unauthorized("User not found");
            }
            if (stockIds.Count == 0)
            {
                return BadRequest("Stock ids list is empty");
            }
            bool? result = await _userRepository.DeleteUserStock(stockIds, user);
            if (result == null || !result.Value)
            {
                return BadRequest("Failed to delete stock");
            }
            return Ok("Stock deleted successfully");
        }
    }
}