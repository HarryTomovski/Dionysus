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
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Rating> Ratings { get; set; }
        public virtual DbSet<Sensor> Sensors { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Dionysus;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.Property(e => e.FinishedStorage).HasDefaultValueSql("('0001-01-01T00:00:00.000')");
            });

            modelBuilder.Entity<ElevationCode>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK__elevatio__357D4CF8CA566050");

                entity.Property(e => e.Code).IsUnicode(false);
            });

            modelBuilder.Entity<EnvironmentalController>(entity =>
            {
                entity.HasKey(e => e.ControllerPinNumber)
                    .HasName("PK__environm__60134B8905AAD4FA");

                entity.Property(e => e.ControllerPinNumber).ValueGeneratedNever();

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.EnvironmentalControllers)
                    .HasForeignKey(d => d.BatchId)
                    .HasConstraintName("FK__environme__batch__60A75C0F");
            });

            modelBuilder.Entity<EnvironmentalReading>(entity =>
            {
                entity.HasKey(e => e.ReadingId)
                    .HasName("PK__environm__8091F95A99860674");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.EnvironmentalReadings)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__environme__batch__6B24EA82");

                entity.HasOne(d => d.SensorPinNumberNavigation)
                    .WithMany(p => p.EnvironmentalReadings)
                    .HasForeignKey(d => d.SensorPinNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__environme__senso__6C190EBB");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.Resolved).HasDefaultValueSql("('FALSE')");

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__notificat__batch__01142BA1");

                entity.HasOne(d => d.Reading)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.ReadingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__notificat__readi__02084FDA");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.Property(e => e.Username).IsUnicode(false);

                entity.HasOne(d => d.Batch)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.BatchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__rating__batch_id__6754599E");

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.Ratings)
                    .HasForeignKey(d => d.Username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__rating__username__68487DD7");
            });

            modelBuilder.Entity<Sensor>(entity =>
            {
                entity.HasKey(e => e.SensorPinNumber)
                    .HasName("PK__sensor__22905D0D8B7D05E3");

                entity.Property(e => e.SensorPinNumber).ValueGeneratedNever();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK___user__F3DBC5735A13B595");

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
