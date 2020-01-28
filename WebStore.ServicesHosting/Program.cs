using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.ServicesHosting.Data;

namespace WebStore.ServicesHosting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<WebStoreContext>();
                    DbInitializer.Initialize(context);

                    var roleStore = new RoleStore<IdentityRole>(context);
                    var roleManager = new RoleManager<IdentityRole>(roleStore,
                        new IRoleValidator<IdentityRole>[] { },
                        new UpperInvariantLookupNormalizer(),
                        new IdentityErrorDescriber(), null);

                    if (!roleManager.RoleExistsAsync("User").Result)
                    {
                        var role = new IdentityRole("User");
                        var result = roleManager.CreateAsync(role).Result;
                    }
                    if (!roleManager.RoleExistsAsync("Administrator").Result)
                    {
                        var role = new IdentityRole("Administrator");
                        var result = roleManager.CreateAsync(role).Result;
                    }

                    var userStore = new UserStore<User>(context);
                    var userManager = new UserManager<User>(userStore,
                        new OptionsManager<IdentityOptions>(
                            new OptionsFactory<IdentityOptions>(
                                new IConfigureOptions<IdentityOptions>[] { },
                                new IPostConfigureOptions<IdentityOptions>[] { })),
                        new PasswordHasher<User>(),
                        new IUserValidator<User>[] { },
                        new IPasswordValidator<User>[] { },
                        new UpperInvariantLookupNormalizer(),
                        new IdentityErrorDescriber(), null, null);

                    if (userStore.FindByEmailAsync("admin@mail.com", CancellationToken.None).Result == null)
                    {
                        var user = new User() { UserName = "Admin", Email = "admin@mail.com" };
                        var result = userManager.CreateAsync(user, "admin").Result;
                        if (result == IdentityResult.Success)
                        {
                            var roleResult = userManager.AddToRoleAsync(user, "Administrator").Result;
                        }
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
