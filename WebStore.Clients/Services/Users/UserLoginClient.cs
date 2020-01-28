using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Dto.User;
using WebStore.Domain.Entities;

namespace WebStore.Clients.Services.Users
{
    public partial class UsersClient : IUserLoginStore<User>
    {
        public Task AddLoginAsync(User user, UserLoginInfo login,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/login/addLogin";
            return PostAsync(url, new AddLoginDto()
            {
                User = user,
                UserLoginInfo = login
            });
        }

        public Task RemoveLoginAsync(User user, string loginProvider, string
            providerKey, CancellationToken cancellationToken)
        {
            var url =
                $"{ServiceAddress}/login/removeLogin/{loginProvider}/{providerKey}";
            return PostAsync(url, user);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user,
            CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/login/getLogins";
            var result = await PostAsync(url, user);
            return await result.Content.ReadAsAsync<List<UserLoginInfo>>();
        }

        public Task<User> FindByLoginAsync(string loginProvider, string
            providerKey, CancellationToken cancellationToken)
        {
            var url =
                $"{ServiceAddress}/login/user/findbylogin/{loginProvider}/{providerKey}";
            return GetAsync<User>(url);
        }
    }
}
