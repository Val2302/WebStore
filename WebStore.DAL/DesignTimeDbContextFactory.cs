using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;

namespace WebStore.DAL
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<WebStoreContext>
    {
        public WebStoreContext CreateDbContext( string[] args )
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath( System.IO.Directory.GetCurrentDirectory() )
                .AddJsonFile( "appsettings.json" )
                .Build();

            var builder = new DbContextOptionsBuilder<WebStoreContext>();

            var connectionString = configuration.GetConnectionString( "DefaultConnection" );

            builder.UseSqlServer( connectionString );

            return new WebStoreContext( builder.Options );
        }
    }
}
