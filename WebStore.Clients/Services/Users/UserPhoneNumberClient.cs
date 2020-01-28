using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities;

namespace WebStore.Clients.Services.Users
{
    public partial class UsersClient : IUserPhoneNumberStore<User>
    {
        public Task SetPhoneNumberAsync(User user, string phoneNumber,
            CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            var url = $"{ServiceAddress}/phonenumber/setPhoneNumber/{phoneNumber}";
            return PostAsync(url, user);
        }

        public async Task<string> GetPhoneNumberAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/phonenumber/getPhoneNumber";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<string>();
        }

        public async Task<bool> GetPhoneNumberConfirmedAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/phonenumber/getPhoneNumberConfirmed";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<bool>();
        }

        public Task SetPhoneNumberConfirmedAsync(User user, bool confirmed,
            CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            var url = $"{ServiceAddress}/phonenumber/setPhoneNumberConfirmed/{confirmed}";
            return PostAsync(url, user);
        }
    }
}
