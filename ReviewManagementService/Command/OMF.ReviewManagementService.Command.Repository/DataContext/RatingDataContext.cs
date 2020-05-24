using Microsoft.EntityFrameworkCore;

namespace OMF.ReviewManagementService.Command.Repository.DataContext
{
    public class RatingDataContext : DbContext
    {
        public RatingDataContext(DbContextOptions<RatingDataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblRating> TblRating { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblRating>(entity =>
            {
                entity.ToTable("tblRating");

                entity.Property(e => e.Id).HasColumnName("ID");


                entity.Property(e => e.Comments)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Rating)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecordTimeStamp)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.RecordTimeStampCreated)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblCustomerId)
                    .HasColumnName("tblCustomerId")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TblRestaurantId)
                    .HasColumnName("tblRestaurantID")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UserCreated).HasDefaultValueSql("((0))");

                entity.Property(e => e.UserModified).HasDefaultValueSql("((0))");
            });
        }
    }
}