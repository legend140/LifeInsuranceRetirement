using LifeInsuranceRetirement.Core;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LifeInsuranceRetirement.Data
{
    public class LifeInsuranceRetirementDbContext : DbContext
    {
        public LifeInsuranceRetirementDbContext(DbContextOptions<LifeInsuranceRetirementDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Benefit>(entity =>
            {
                entity.ToTable("Benefits");
                entity.Property(b => b.ConsumerId).IsRequired().HasColumnType("int");
                entity.Property(b => b.ConfigurationId).IsRequired().HasColumnType("int");
                entity.HasIndex(b => new { b.ConsumerId }).HasDatabaseName("IX_Benefit_Consumer");
                entity.HasIndex(b => new { b.ConfigurationId }).HasDatabaseName("IX_Benefit_Configuration");

                entity.HasOne<Consumer>(b => b.Consumer)
                    .WithMany(c => c.Benefits)
                    .HasForeignKey(b => b.ConsumerId)
                    .HasConstraintName("FK_Benefit_Consumer")
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<Configuration>(b => b.Configuration)
                    .WithMany()
                    .HasForeignKey(b => b.ConfigurationId)
                    .HasConstraintName("FK_Benefit_Configuration")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BenefitDetail>(entity =>
            {
                entity.ToTable("BenefitDetails");
                entity.HasKey(bd => new { bd.Multiple, bd.BenefitId }).HasName("PK_BenefitDetail");
                entity.Property(bd => bd.BenefitId).IsRequired().HasColumnType("int");
                entity.HasIndex(bd => new { bd.BenefitId }).HasDatabaseName("IX_Benefit_BenefitDetail");

                entity.HasOne<Benefit>(bd => bd.Benefit)
                    .WithMany(b => b.BenefitDetails)
                    .HasForeignKey(b => b.BenefitId)
                    .HasConstraintName("FK_Benefit_Detail")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ConsumerLogs>(entity =>
            {
                entity.HasOne<Benefit>(l => l.Benefit)
                    .WithMany(c => c.ConsumerLogs)
                    .HasForeignKey(l => l.BenefitId)
                    .HasConstraintName("FK_Benefit_ConsumerLogs")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Benefit>().Navigation(e => e.Configuration).AutoInclude();
        }

        public DbSet<Configuration> Configuration { get; set; }
        public DbSet<ConfigurationLogs> ConfigurationLogs { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<ConsumerLogs> ConsumerLogs { get; set; }
        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<BenefitDetail> BenefitDetails { get; set; }
    }
}
