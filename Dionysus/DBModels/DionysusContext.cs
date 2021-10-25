using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Dionysus.DBModels
{
    public partial class DionysusContext : DbContext
    {
        public DionysusContext()
        {
        }

        public DionysusContext(DbContextOptions<DionysusContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EnvironmentalReading> EnvironmentalReadings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=Dionysus;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<EnvironmentalReading>(entity =>
            {
                entity.HasKey(e => e.ReadingId)
                    .HasName("PK__environm__8091F95AAEBBA4D7");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
