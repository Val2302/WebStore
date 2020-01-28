using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.ServicesHosting.Controllers.Users.Base;

namespace WebStore.ServicesHosting.Controllers.Users
{
    [Route("api/users/twofactor"),
     Produces("application/json")]
    public class UserTwoFactorApiController : BaseUserApiController
    {
        public UserTwoFactorApiController(WebStoreContext context) : base(context)
        {
        }

        [HttpPost("setTwoFactor/{enabled}")]
        public async Task SetTwoFactorEnabledAsync([FromBody]User user, bool
            enabled)
        {
            await _userStore.SetTwoFactorEnabledAsync(user, enabled);
        }

        [HttpPost("getTwoFactorEnabled")]
        public async Task<bool> GetTwoFactorEnabledAsync([FromBody]User user)
        {
            return await _userStore.GetTwoFactorEnabledAsync(user);
        }
    }
}