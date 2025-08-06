using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MentorAi_backd.Infrastructure.Persistance.Data;

namespace MentorAi_backd.Infrastructure
{
    public class MentorAiDbContextFactory : IDesignTimeDbContextFactory<MentorAiDbContext>
    {
        public MentorAiDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MentorAiDbContext>();

            // Update the connection string as needed
            optionsBuilder.UseSqlServer("Server=ZAINSPC\\MSSQLSERVER01;Database=MentorAiDataBase02;Trusted_Connection=True;TrustServerCertificate=True;");

            return new MentorAiDbContext(optionsBuilder.Options);
        }
    }
}