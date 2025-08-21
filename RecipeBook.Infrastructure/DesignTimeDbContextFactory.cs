using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace RecipeBook.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<RecipeBookDbContext>
    {
        public RecipeBookDbContext CreateDbContext(string[] args)
        {
            // Use current directory to load configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Use current directory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<RecipeBookDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("dbcs"));

            return new RecipeBookDbContext(optionsBuilder.Options);
        }
    }
}
