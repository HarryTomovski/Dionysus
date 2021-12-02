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

        public virtual DbSet<Batch> Batches { get; set; }
        public virtual DbSet<ElevationCode> ElevationCodes { get; set; }
        public virtual DbSet<EnvironmentalController> EnvironmentalControllers { get; set; }
        public virtual DbSet<EnvironmentalReading> EnvironmentalReadings { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Sensor> Sensors { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=Dionysus;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ElevationCode>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__elevatio__357D4CF87BB60622");

                entity.Property(e => e.Code).IsUnicode(false);
            });

            modelBuilder.Entity<EnvironmentalController>(entity =>
            {
                entity.HasKey(e => e.ControllerPinNumber)
                    .HasName("PK__environm__60134B892EDAB51A");

                entity.Property(e => e.ControllerPinNumber).ValueGeneratedNever();

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.EnvironmentalControllers)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK__environme__batch__286302EC");
            });

            modelBuilder.Entity<EnvironmentalReading>(entity =>
            {
                entity.HasKey(e => e.ReadingId)
                    .HasName("PK__environm__8091F95A9A177427");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.EnvironmentalReadings)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__environme__batch__32E0915F");

                entity.HasOne(d => d.SensorPinNumberNavigation)
                    .WithMany(p => p.EnvironmentalReadings)
                    .HasForeignKey(d => d.SensorPinNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__environme__senso__33D4B598");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.Property(e => e.Username).IsUnicode(false);

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__rating__batch_id__2F10007B");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__rating__username__300424B4");
            });

            modelBuilder.Entity<Sensor>(entity =>
            {
                entity.HasKey(e => e.SensorPinNumber)
                    .HasName("PK__sensor__22905D0D5367A191");

                entity.Property(e => e.SensorPinNumber).ValueGeneratedNever();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK___user__F3DBC573A7E3E165");

                entity.Property(e => e.Username).IsUnicode(false);

                entity.Property(e => e.Name).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Role).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
