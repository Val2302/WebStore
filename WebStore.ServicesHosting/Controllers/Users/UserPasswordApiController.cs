using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.DAL.Context;
using WebStore.Domain.Dto.User;
using WebStore.Domain.Entities;
using WebStore.ServicesHosting.Controllers.Users.Base;

namespace WebStore.ServicesHosting.Controllers.Users
{
    [Route("api/users/password"),
     Produces("application/json")]
    public class UserPasswordApiController : BaseUserApiController
    {
        public UserPasswordApiController(WebStoreContext context) : base(context)
        {
        }

        [HttpPost("setPasswordHash")]
        public async Task<string> SetPasswordHashAsync([FromBody]PasswordHashDto
            hashDto)
        {
            await _userStore.SetPasswordHashAsync(hashDto.User, hashDto.Hash);
            return hashDto.User.PasswordHash;
        }
        [HttpPost("getPasswordHash")]
        public async Task<string> GetPasswordHashAsync([FromBody]User user)
        {
            var result = await _userStore.GetPasswordHashAsync(user);
            return result;
        }

        [HttpPost("hasPassword")]
        public async Task<bool> HasPasswordAsync([FromBody]User user)
        {
            return await _userStore.HasPasswordAsync(user);
        }
    }
}