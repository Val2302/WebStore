using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.DAL.Context;
using WebStore.Domain.Dto.User;
using WebStore.Domain.Entities;
using WebStore.ServicesHosting.Controllers.Users.Base;

namespace WebStore.ServicesHosting.Controllers.Users
{
    [Route("api/users/login"),
     Produces("application/json")]
    public class UserLoginApiController : BaseUserApiController
    {
        public UserLoginApiController(WebStoreContext context) : base(context)
        {
        }

        [HttpPost("addLogin")]
        public async Task AddLoginAsync([FromBody]AddLoginDto loginDto)
        {
            await _userStore.AddLoginAsync(loginDto.User,
                loginDto.UserLoginInfo);
        }

        [HttpPost("removeLogin/{loginProvider}/{providerKey}")]
        public async Task RemoveLoginAsync([FromBody]User user, string
            loginProvider, string providerKey)
        {
            await _userStore.RemoveLoginAsync(user, loginProvider, providerKey);
        }

        [HttpPost("getLogins")]
        public async Task<IList<UserLoginInfo>> GetLoginsAsync([FromBody]User
            user)
        {
            return await _userStore.GetLoginsAsync(user);
        }

        [HttpGet("user/findbylogin/{loginProvider}/{providerKey}")]
        public async Task<User> FindByLoginAsync(string loginProvider, string
            providerKey)
        {
            return await _userStore.FindByLoginAsync(loginProvider,
                providerKey);
        }
    }
}