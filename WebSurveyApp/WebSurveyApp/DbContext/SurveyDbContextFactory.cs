using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebSurveyApp
{
    class SurveyDbContextFactory : IDbContextFactory<SurveyDbContext>
    {
        public IConfiguration Config { get; set; }
        public SurveyDbContextFactory(IConfiguration config)
        {
            Config = config;
        }
        public SurveyDbContext CreateDbContext()
        {
            var connectionString = Config.GetConnectionString("DefaultConnection");

            var optionBuilder = new DbContextOptionsBuilder();
            var option = optionBuilder
                .UseSqlServer(connectionString)
                .Options;

            return new SurveyDbContext(option);
        }
    }
}
