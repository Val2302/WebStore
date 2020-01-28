using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.ServicesHosting.Controllers.Users.Base;

namespace WebStore.ServicesHosting.Controllers.Users
{
    [Route("api/users/role"),
     Produces("application/json")]
    public class UserRoleApiController : BaseUserApiController
    {
        public UserRoleApiController(WebStoreContext context) : base(context)
        {
        }

        [HttpPost("role/{roleName}")]
        public async Task AddToRoleAsync([FromBody]User user, string roleName)
        {
            await _userStore.AddToRoleAsync(user, roleName);
        }

        [HttpPost("role/delete/{roleName}")]
        public async Task RemoveFromRoleAsync([FromBody]User user, string
            roleName)
        {
            await _userStore.RemoveFromRoleAsync(user, roleName);
        }

        [HttpPost("roles")]
        public async Task<IList<string>> GetRolesAsync([FromBody]User user)
        {
            return await _userStore.GetRolesAsync(user);
        }

        [HttpPost("inrole/{roleName}")]
        public async Task<bool> IsInRoleAsync([FromBody]User user, string
            roleName)
        {
            return await _userStore.IsInRoleAsync(user, roleName);
        }
        [HttpGet("usersInRole/{roleName}")]
        public async Task<IList<User>> GetUsersInRoleAsync(string roleName)
        {
            return await _userStore.GetUsersInRoleAsync(roleName);
        }
    }
}