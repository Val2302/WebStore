using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Dto.User;
using WebStore.Domain.Entities;

namespace WebStore.Clients.Services.Users
{
    public partial class UsersClient : IUserPasswordStore<User>
    {
        public async Task SetPasswordHashAsync(User user, string passwordHash,
            CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            var url = $"{ServiceAddress}/password/setPasswordHash";
            await PostAsync(url, new PasswordHashDto()
            {
                User = user,
                Hash = passwordHash
            });
        }

        public async Task<string> GetPasswordHashAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/password/getPasswordHash";
            var result = await PostAsync(url, user);
            var ret = await result.Content.ReadAsAsync<string>();
            return ret;
        }

        public async Task<bool> HasPasswordAsync(User user, CancellationToken
            cancellationToken)
        {
            var url = $"{ServiceAddress}/password/hasPassword";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }
    }
}
