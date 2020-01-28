using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities;

namespace WebStore.Clients.Services.Users
{
    public partial class UsersClient : IUserEmailStore<User>
    {
        public Task SetEmailAsync(User user, string email, CancellationToken
            cancellationToken)
        {
            user.Email = email;
            var url = $"{ServiceAddress}/email/setEmail/{email}";
            return PostAsync(url, user);
        }

        public async Task<string> GetEmailAsync(User user, CancellationToken
            cancellationToken)
        {
            var url = $"{ServiceAddress}/email/getEmail";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }

        public async Task<bool> GetEmailConfirmedAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/email/getEmailConfirmed";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }

        public Task SetEmailConfirmedAsync(User user, bool confirmed,
            CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            var url = $"{ServiceAddress}/email/setEmailConfirmed/{confirmed}";
            return PostAsync(url, user);
        }

        public Task<User> FindByEmailAsync(string normalizedEmail,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/email/user/findByEmail/{normalizedEmail}";
            return GetAsync<User>(url);
        }

        public async Task<string> GetNormalizedEmailAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/email/getNormalizedEmail";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }

        public Task SetNormalizedEmailAsync(User user, string normalizedEmail,
            CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            var url = $"{ServiceAddress}/email/setEmail/{normalizedEmail}";
            return PostAsync(url, user);
        }
    }
}
