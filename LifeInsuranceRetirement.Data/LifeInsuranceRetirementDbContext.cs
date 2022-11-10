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
            modelBuilder.Entity<Benefits>(entity =>
            {
                entity.ToTable("Benefits");
                entity.HasKey(b => new { b.ConsumerId, b.ConfigurationId, b.Multiple }).HasName("PK_Benefits");
                entity.Property(b => b.ConsumerId).IsRequired().HasColumnType("int");
                entity.Property(b => b.ConfigurationId).IsRequired().HasColumnType("int");
                entity.HasIndex(b => new { b.ConsumerId }).HasDatabaseName("IX_Benefits_Consumer");
                entity.HasIndex(b => new { b.ConfigurationId }).HasDatabaseName("IX_Benefits_Configuration");

                entity.HasOne<Consumer>(b => b.Consumer)
                    .WithMany(c => c.Benefits)
                    .HasForeignKey(b => b.ConsumerId)
                    .HasConstraintName("FK_Benefits_Consumer")
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne<Configuration>(b => b.Configuration)
                    .WithMany()
                    .HasForeignKey(b => b.ConfigurationId)
                    .HasConstraintName("FK_Benefits_Configuration")
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Benefits>().Navigation(e => e.Configuration).AutoInclude();
        }

        public DbSet<Configuration> Configuration { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<Benefits> Benefits { get; set; }
    }
}
