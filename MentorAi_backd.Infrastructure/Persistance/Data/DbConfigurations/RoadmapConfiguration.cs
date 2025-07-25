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
    public class RoadmapConfiguration : IEntityTypeConfiguration<Roadmap>
    {
        public void Configure(EntityTypeBuilder<Roadmap> builder)
        {
            builder.HasKey(r => r.Id);

           
            builder.HasMany(r =>r.Progresses)
                .WithOne(srp => srp.Roadmap)
                .HasForeignKey(srp => srp.RoadmapId)
                .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}
