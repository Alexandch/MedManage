using MedManage.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MedManage.Persistence.Data
{
    public class AnnouncementDbContext : DbContext
    {
        public DbSet<Announcement> Announcements { get; set; }

        public AnnouncementDbContext(DbContextOptions<AnnouncementDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.HasKey(a => a.AnnouncementId); // Primary Key

                entity.Property(a => a.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(a => a.Content)
                    .IsRequired()
                    .HasMaxLength(2000);

                entity.Property(a => a.CreatedAt)
                    .IsRequired();

                entity.Property(a => a.ExpirationDate)
                    .IsRequired(false);

                entity.HasOne(a => a.CreatedByUser)
                    .WithMany() // Assuming a user can create multiple announcements
                    .HasForeignKey(a => a.CreatedByUserId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);


                
                entity.Property(a => a.StatusInventory).IsRequired().HasConversion<int>();
                
                entity.Property(a => a.TypeProduct).IsRequired().HasConversion<int>();
            });
        }
    }
}
