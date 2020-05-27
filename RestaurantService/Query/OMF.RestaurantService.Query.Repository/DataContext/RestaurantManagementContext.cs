using Microsoft.EntityFrameworkCore;

namespace OMF.RestaurantService.Query.Repository.DataContext
{
    public partial class RestaurantManagementContext : DbContext
    {

        public RestaurantManagementContext(DbContextOptions<RestaurantManagementContext> options)
            : base(options)
        {
        }
        //private readonly string _connectionString;
        //public RestaurantManagementContext(string connectionString):base()
        //{
        //    _connectionString = connectionString;
        //}
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if(!optionsBuilder.IsConfigured)
        //        optionsBuilder.UseSqlServer(_connectionString);
        //}
        
        public virtual DbSet<TblCuisine> TblCuisine { get; set; }
        public virtual DbSet<TblLocation> TblLocation { get; set; }
        public virtual DbSet<TblMenu> TblMenu { get; set; }
        public virtual DbSet<TblOffer> TblOffer { get; set; }
        public virtual DbSet<TblRestaurant> TblRestaurant { get; set; }
        public virtual DbSet<TblRestaurantDetails> TblRestaurantDetails { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblCuisine>(entity =>
            {
                entity.ToTable("tblCuisine");

                entity.Property(e => e.Id).HasColumnName("ID").UseIdentityColumn();

                entity.Property(e => e.Cuisine)
                    .IsRequired()
                    .HasMaxLength(225)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<TblLocation>(entity =>
            {
                entity.ToTable("tblLocation");

                entity.HasIndex(e => e.X)
                    .HasName("UQ__tblLocat__3BD0198414754610")
                    .IsUnique();

                entity.HasIndex(e => e.Y)
                    .HasName("UQ__tblLocat__3BD01987EC697B94")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID").UseIdentityColumn();

                entity.Property(e => e.X).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Y).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<TblMenu>(entity =>
            {
                entity.ToTable("tblMenu");

                entity.Property(e => e.Id).HasColumnName("ID").UseIdentityColumn();

                entity.Property(e => e.Item)
                    .IsRequired()
                    .HasMaxLength(225)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TblCuisineId)
                    .HasColumnName("tblCuisineID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserModified).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.TblCuisine)
                    .WithMany(p => p.TblMenu)
                    .HasForeignKey(d => d.TblCuisineId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblMenu_tblCuisineID");

                entity.Property(e => e.Quantity)
                    .HasColumnName("Quantity")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TblOffer>(entity =>
            {
                entity.ToTable("tblOffer");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Discount).HasDefaultValueSql("((0))");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Price).HasColumnType("decimal").HasDefaultValueSql("((0))");

                entity.Property(e => e.RecordTimeStampCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblMenuId)
                    .HasColumnName("tblMenuID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblRestaurantId)
                    .HasColumnName("tblRestaurantID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.TblMenu)
                    .WithMany(p => p.TblOffer)
                    .HasForeignKey(d => d.TblMenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblOffer_tblMenuID");

                entity.HasOne(d => d.TblRestaurant)
                    .WithMany(p => p.TblOffer)
                    .HasForeignKey(d => d.TblRestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblOffer_tblRestaurantID");
            });

            modelBuilder.Entity<TblRestaurant>(entity =>
            {
                entity.ToTable("tblRestaurant");

                entity.Property(e => e.Id).HasColumnName("ID").UseIdentityColumn();

                entity.Property(e => e.Address).IsUnicode(false);

                entity.Property(e => e.CloseTime)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ContactNo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(225)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OpeningTime)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TblLocationId)
                    .HasColumnName("tblLocationID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Rating)
                    .IsRequired()
                    .HasColumnType("decimal(3,1)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Budget)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Website)
                    .IsRequired()
                    .HasMaxLength(225)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.ModifiedTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedRecordTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.TblLocation)
                    .WithMany(p => p.TblRestaurant)
                    .HasForeignKey(d => d.TblLocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblRestaurant_tblLocationID");
            });

            modelBuilder.Entity<TblRestaurantDetails>(entity =>
            {
                entity.ToTable("tblRestaurantDetails");

                entity.Property(e => e.Id).HasColumnName("ID").UseIdentityColumn();

                entity.Property(e => e.ModifiedTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedRecordTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TableCapacity).HasDefaultValueSql("((0))");

                entity.Property(e => e.TableCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.TblRestaurantId)
                    .HasColumnName("tblRestaurantID")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.TblRestaurant)
                    .WithMany(p => p.TblRestaurantDetails)
                    .HasForeignKey(d => d.TblRestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblRestaurantDetails_tblRestaurantID");
            });
        }
    }
}