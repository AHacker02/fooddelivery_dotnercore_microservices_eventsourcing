using Microsoft.EntityFrameworkCore;

namespace OMF.OrderManagementService.Command.Repository.DataContext
{
    public partial class OrderManagementContext:DbContext
    {
        public OrderManagementContext(DbContextOptions<OrderManagementContext> context) : base(context)
        {

        }
        

        public virtual DbSet<TblFoodOrder> TblFoodOrder { get; set; }
        public virtual DbSet<TblFoodOrderItem> TblFoodOrderItem { get; set; }
        public virtual DbSet<TblOrderPayment> TblOrderPayment { get; set; }
        public virtual DbSet<TblTableBooking> TblTableBooking { get; set; }
        public virtual DbSet<TblTableDetail> TblTableDetail { get; set; }
        
        
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblFoodOrder>(entity =>
            {
                entity.ToTable("tblFoodOrder");

                entity.Property(e => e.Id).HasColumnName("ID").UseIdentityColumn();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblCustomerId)
                    .HasColumnName("tblCustomerID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Status)
                    .HasColumnName("Status");

                entity.Property(e => e.TblRestaurantId)
                    .HasColumnName("tblRestaurantID")
                    .HasDefaultValueSql("((0))");
                

            });

            modelBuilder.Entity<TblFoodOrderItem>(entity =>
            {
                entity.ToTable("tblFoodOrderItem");

                entity.Property(e => e.Id).HasColumnName("ID").UseIdentityColumn();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblFoodOrderId)
                    .HasColumnName("tblFoodOrderID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblMenuId)
                    .HasColumnName("tblMenuID")
                    .HasDefaultValueSql("((0))");
                entity.Property(e => e.Quantity)
                    .HasColumnName("Quantity");

                entity.HasOne(d => d.TblFoodOrder)
                    .WithMany(p => p.TblFoodOrderItem)
                    .HasForeignKey(d => d.TblFoodOrderId);
            });

            modelBuilder.Entity<TblOrderPayment>(entity =>
            {
                entity.ToTable("tblOrderPayment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Remarks)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TblCustomerId).HasColumnName("tblCustomerID");


                entity.Property(e => e.PaymentStatus).HasColumnName("PaymentStatus");

                entity.Property(e => e.PaymentType).HasColumnName("PaymentType");

                entity.Property(e => e.TransactionId)
                    .IsRequired()
                    .HasColumnName("TransactionID");
                
            });


            modelBuilder.Entity<TblTableBooking>(entity =>
            {
                entity.ToTable("tblTableOrder");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblCustomerId)
                    .HasColumnName("tblCustomerID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Status)
                    .HasColumnName("BookingStatus");

                entity.Property(e => e.TblRestaurantId)
                    .HasColumnName("tblRestaurantID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblTableDetail>(entity =>
            {
                entity.ToTable("tblTableOrderMapping");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TableNo).HasDefaultValueSql("((0))");


                entity.HasOne(d => d.TblTableBooking)
                    .WithMany(p => p.TblTableDetail)
                    .HasForeignKey(d => d.TblTableBookingId)
                    .HasConstraintName("FK_tblTableOrderMapping_tblTableOrderID");
            });
        }

    }
}