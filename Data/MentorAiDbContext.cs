using MentorAi_backd.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace MentorAi_backd.Data
{
    public class MentorAiDbContext : DbContext
    {
        public MentorAiDbContext(DbContextOptions<MentorAiDbContext>options):base(options) { }
        public DbSet<User> Students { get; set; }


        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity &&
                           (e.State == EntityState.Modified || e.State == EntityState.Added));

            foreach (var entry in entries)
            {
                ((BaseEntity)entry.Entity).LastUpdatedAt = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).CreatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

    }
}
