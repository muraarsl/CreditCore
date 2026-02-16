using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CreditCore.Infrastructure.Persistence
{
    public class CreditDbContext : DbContext
    {
        public CreditDbContext(DbContextOptions<CreditDbContext> options)
            : base(options)
        {
        }

        public DbSet<CreditEntity> Credits { get; set; }
        public DbSet<InstallmentEntity> Installments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CreditEntity>(entity =>
            {
                entity.HasKey(x => x.Id).HasName("ID");

                
                entity.Property(x => x.PrincipalAmount)
                .HasColumnName("PRINCIPAL_AMOUNT")
                      .HasPrecision(18, 2);

                entity.Property(x => x.AnnualInterestRate)
                .HasColumnName("ANNUAL_INTEREST_RATE")
                      .HasPrecision(9, 6);

                entity.HasMany(x => x.Installments)
                      .WithOne(x => x.Credit)
                      .HasForeignKey(x => x.CreditId)
                      .OnDelete(DeleteBehavior.Cascade);
                
                entity.Property(x => x.TermInMonths)
                .HasColumnName("TERM_IN_MONTHS")
                      .IsRequired();


                entity.Property(x => x.CreatedAt).HasColumnName("CREATED_AT")
                      .IsRequired();



                modelBuilder.Entity<InstallmentEntity>()
                    .HasIndex(x => new { x.CreditId, x.InstallmentNo })
                    .IsUnique();
       


            });

            modelBuilder.Entity<InstallmentEntity>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.CreditId).HasPrecision(18, 2).HasColumnName("CREDIT_ID");
                entity.Property(x => x.PrincipalPayment).HasPrecision(18, 2).HasColumnName("PRINCIPAL_PAYMENT");
                entity.Property(x => x.InterestPayment).HasPrecision(18, 2).HasColumnName("INTEREST_PAYMENT");
                entity.Property(x => x.Bsmv).HasPrecision(18, 2).HasColumnName("BSMV_AMOUNT"); ;
                entity.Property(x => x.Kkdf).HasPrecision(18, 2).HasColumnName("KKDF_AMOUNT");
                entity.Property(x => x.RemainingBalance).HasPrecision(18, 2).HasColumnName("REMAINING_BALANCE");
                entity.Property(x => x.InstallmentNo).HasColumnName("INSTALLMENT_NO");
            });
        }
    }
}
