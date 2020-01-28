using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Dto.User;
using WebStore.Domain.Entities;

namespace WebStore.Clients.Services.Users
{
    public partial class UsersClient : IUserClaimStore<User>
    {
        public async Task<IList<Claim>> GetClaimsAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/claim/getClaims";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<List<Claim>>();
        }

        public Task AddClaimsAsync(User user, IEnumerable<Claim> claims,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/claim/addClaims";
            return PostAsync(url, new AddClaimsDto()
            {
                User = user,
                Claims = claims
            });
        }

        public Task ReplaceClaimAsync(User user, Claim claim, Claim newClaim,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/claim/replaceClaim";
            return PostAsync(url, new ReplaceClaimsDto()
            {
                User = user,
                Claim = claim,
                NewClaim = newClaim
            });
        }

        public Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/claim/removeClaims";
            return PostAsync(url, new RemoveClaimsDto()
            {
                User = user,
                Claims = claims
            });
        }

        public async Task<IList<User>> GetUsersForClaimAsync(Claim claim,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/claim/getUsersForClaim";
            var result = await PostAsync(url, claim);
            return await result.Content.ReadAsAsync<List<User>>();
        }
    }
}
