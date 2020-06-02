using Microsoft.EntityFrameworkCore;

namespace OMF.CustomerManagementService.Command.Repository.DataContext
{
    public class CustomerManagementContext : DbContext
    {
        public CustomerManagementContext(DbContextOptions<CustomerManagementContext> context) : base(context)
        {

        }

        public virtual DbSet<TblCustomer> TblCustomer { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCustomer>(entity =>
            {
                entity.ToTable("tblCustomer");

                entity.Property(e => e.Id).HasColumnName("ID").UseIdentityColumn();

                entity.Property(e => e.Address);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(225)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(225)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(225)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Password)
                    .IsRequired();

                entity.Property(e => e.PasswordKey)
                    .IsRequired();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");
            });
        }
    }
}