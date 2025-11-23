using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Mapster;
namespace backend.Dtos.User
{
    public class UserDisplayDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
    public static class UserDisplayDtoMapper
    {
        public static UserDisplayDto ToDto(this DefaultUser user)
        {
            UserDisplayDto userDisplayDto = user.Adapt<UserDisplayDto>();
            return userDisplayDto;
        }
    }
}