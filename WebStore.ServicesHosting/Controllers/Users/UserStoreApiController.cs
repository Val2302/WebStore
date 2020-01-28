using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.ServicesHosting.Controllers.Users.Base;

namespace WebStore.ServicesHosting.Controllers.Users
{
    [Route("api/users/user"),
    Produces("application/json")]
    public class UserStoreApiController : BaseUserApiController
    {
        public UserStoreApiController(WebStoreContext context) : base(context)
        {
        }

        [HttpPost("userId")]
        public async Task<string> GetUserIdAsync([FromBody]User user)
        {
            return await _userStore.GetUserIdAsync(user);
        }

        [HttpPost("userName")]
        public async Task<string> GetUserNameAsync([FromBody]User user)
        {
            return await _userStore.GetUserNameAsync(user);
        }

        [HttpPost("userName/{userName}")]
        public async Task SetUserNameAsync([FromBody]User user, string userName)
        {
            await _userStore.SetUserNameAsync(user, userName);
        }

        [HttpPost("normalUserName")]
        public async Task<string> GetNormalizedUserNameAsync([FromBody]User
            user)
        {
            var result = await _userStore.GetNormalizedUserNameAsync(user);
            return result;
        }

        [HttpPost("normal/{normalizedName}")]
        public async Task SetNormalizedUserNameAsync([FromBody]User user, string
            normalizedName)
        {
            await _userStore.SetNormalizedUserNameAsync(user, normalizedName);
        }

        [HttpPost]
        public async Task<bool> CreateAsync([FromBody]User user)
        {
            var result = await _userStore.CreateAsync(user);
            return result.Succeeded;
        }

        [HttpPut]
        public async Task<bool> UpdateAsync([FromBody]User user)
        {
            var result = await _userStore.UpdateAsync(user);
            return result.Succeeded;
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _userStore.FindByIdAsync(id);

            if (user is null) return false;

            var result = await _userStore.DeleteAsync(user);
            return result.Succeeded;
        }
        [HttpGet("{userId}")]
        public async Task<User> FindByIdAsync(string userId)
        {
            var result = await _userStore.FindByIdAsync(userId);
            return result;
        }

        [HttpGet("normal/{normalizedUserName}")]
        public async Task<User> FindByNameAsync(string normalizedUserName)
        {
            var result = await _userStore.FindByNameAsync(normalizedUserName);
            return result;
        }
    }
}