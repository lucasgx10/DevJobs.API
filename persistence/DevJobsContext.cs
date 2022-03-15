using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevJobs.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevJobs.API.persistence
{
    public class DevJobsContext : DbContext
    {
        public DevJobsContext(DbContextOptions<DevJobsContext> options) : base (options)
        {
          
        }
        public DbSet<JobVacancy> JobVacancies {get; set;}
        public DbSet<JobApplication> JobApplications {get; set;}
        protected override void OnModelCreating(ModelBuilder builder) {
            
            builder.Entity<JobVacancy>(e => {
                e.HasKey(jv => jv.Id); //chave primaria
                e.HasMany(jv => jv.Applications) //relacionamento
                .WithOne() //somente 1
                .HasForeignKey(ja => ja.IdJobVacancy)//chave esrangeira
                .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<JobApplication>(e => {
                e.HasKey(ja => ja.Id);
            });
        }
    }
}

