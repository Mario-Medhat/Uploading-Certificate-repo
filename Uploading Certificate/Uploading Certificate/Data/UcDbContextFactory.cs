using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Uploading_Certificate.Data
{
    public class UcDbContextFactory : IDesignTimeDbContextFactory<UcDbContext>
    {
        public UcDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString =
                configuration.GetConnectionString("UcConnectionString");

            var optionsBuilder = new DbContextOptionsBuilder<UcDbContext>();

            optionsBuilder.UseSqlServer(connectionString);

            return new UcDbContext(optionsBuilder.Options);
        }
    }
}
