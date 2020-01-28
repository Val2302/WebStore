using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.DAL.Context;
using WebStore.Domain.Dto.User;
using WebStore.Domain.Entities;
using WebStore.ServicesHosting.Controllers.Users.Base;

namespace WebStore.ServicesHosting.Controllers.Users
{
    [Route("api/users/claim"),
     Produces("application/json")]
    public class UserClaimApiController : BaseUserApiController
    {
        public UserClaimApiController(WebStoreContext context) : base(context)
        {
        }

        [HttpPost("getClaims")]
        public async Task<IList<Claim>> GetClaimsAsync([FromBody]User user)
        {
            return await _userStore.GetClaimsAsync(user);
        }

        [HttpPost("addClaims")]
        public async Task AddClaimsAsync([FromBody]AddClaimsDto claimsDto)
        {
            await _userStore.AddClaimsAsync(claimsDto.User, claimsDto.Claims);
        }

        [HttpPost("replaceClaim")]
        public async Task ReplaceClaimAsync([FromBody]ReplaceClaimsDto
            claimsDto)
        {
            await _userStore.ReplaceClaimAsync(claimsDto.User, claimsDto.Claim,
                claimsDto.NewClaim);
        }

        [HttpPost("removeClaims")]
        public async Task RemoveClaimsAsync([FromBody]RemoveClaimsDto claimsDto)
        {
            await _userStore.RemoveClaimsAsync(claimsDto.User,
                claimsDto.Claims);
        }

        [HttpPost("getUsersForClaim")]
        public async Task<IList<User>> GetUsersForClaimAsync([FromBody]Claim
            claim)
        {
            return await _userStore.GetUsersForClaimAsync(claim);
        }
    }
}
