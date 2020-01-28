using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Dto.User;
using WebStore.Domain.Entities;

namespace WebStore.Clients.Services.Users
{
    public partial class UsersClient : IUserLockoutStore<User>
    {
        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/lockout/getLockoutEndDate";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<DateTimeOffset?>();
        }

        public Task SetLockoutEndDateAsync(User user, DateTimeOffset?
            lockoutEnd, CancellationToken cancellationToken)
        {
            user.LockoutEnd = lockoutEnd;
            var url = $"{ServiceAddress}/lockout/setLockoutEndDate";
            return PostAsync(url, new SetLockoutDto()
            {
                User = user,
                LockoutEnd
                    = lockoutEnd
            });
        }

        public async Task<int> IncrementAccessFailedCountAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/lockout/IncrementAccessFailedCount";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<int>();
        }

        public Task ResetAccessFailedCountAsync(User user, CancellationToken
            cancellationToken)
        {
            var url = $"{ServiceAddress}/lockout/ResetAccessFailedCount";
            return PostAsync(url, user);
        }

        public async Task<int> GetAccessFailedCountAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/lockout/GetAccessFailedCount";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<int>();
        }

        public async Task<bool> GetLockoutEnabledAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/lockout/GetLockoutEnabled";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }

        public async Task SetLockoutEnabledAsync(User user, bool enabled,
            CancellationToken cancellationToken)
        {
            user.LockoutEnabled = enabled;
            var url = $"{ServiceAddress}/lockout/SetLockoutEnabled/{enabled}";
            await PostAsync(url, user);
        }
    }
}
