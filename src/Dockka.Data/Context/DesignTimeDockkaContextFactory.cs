using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Dockka.Data.Context
{
    public class DesignTimeDockkaContextFactory : IDesignTimeDbContextFactory<DockkaContext>
    {
        public DockkaContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();

            var builder = new DbContextOptionsBuilder<DockkaContext>();

            var connectionString = configuration.GetConnectionString("DockkaSqlConnection");
            
            builder.UseSqlServer(connectionString);

            return new DockkaContext(builder.Options);
        }
    }
}
