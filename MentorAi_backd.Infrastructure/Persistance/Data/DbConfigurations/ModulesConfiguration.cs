using MentorAi_backd.Domain.Entities.UserEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentorAi_backd.Infrastructure.Persistance.Data.DbConfigurations
{
    public class ModulesConfiguration : IEntityTypeConfiguration<LearningModule>
    {
        public void Configure(EntityTypeBuilder<LearningModule> builder)
        {
            builder.HasKey(m => m.Id);
            
            builder.HasMany(m=>m.Problems)
                .WithOne(p=> p.LearningModule)
                .HasForeignKey(p => p.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
    
}
