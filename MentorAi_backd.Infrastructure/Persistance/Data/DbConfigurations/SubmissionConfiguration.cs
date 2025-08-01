using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentorAi_backd.Domain.Entities.Problems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MentorAi_backd.Infrastructure.Persistance.Data.DbConfigurations
{
    public class SubmissionConfiguration : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> builder)
        {
            builder.HasOne(p=>p.Problem)
                .WithMany(s=>s.Submission)
                .HasForeignKey(s=>s.ProblemId);

            builder.HasOne(p => p.Student)
                .WithMany(s => s.Submissions)
                .HasForeignKey(s => s.StudentId);

        }
    }
}
