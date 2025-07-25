using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Domain.Entities.UserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorAi_backd.Infrastructure.Persistance.Data.DbConfigurations
{
    public class RoadmapModuleConfiguration : IEntityTypeConfiguration<RoadmapModule>
    
    {
        public void Configure(EntityTypeBuilder<RoadmapModule> builder)
        {
            builder.HasKey(rm => new { rm.RoadmapId, rm.ModuleId });

            builder.HasOne(rm => rm.Roadmap)
                .WithMany(r => r.RoadmapModules)
                .HasForeignKey(rm => rm.RoadmapId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(rm => rm.LearningModule)
                .WithMany(m => m.RoadmapModules)
                .HasForeignKey(rm => rm.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
