using CAR_RENTAL_MS_III.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAR_RENTAL_MS_III.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        public DbSet<CarCategory> CarCategories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer to Rental - One-to-Many
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Rentals)
                .WithOne(r => r.Customer)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);  // Set Cascade Delete if Customer is deleted

            // Car to Rental - One-to-Many
            modelBuilder.Entity<Car>()
                .HasMany(c => c.Rentals)
                .WithOne(r => r.Car)
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict deletion if Car has rentals

           
            // Manager to Rental - One-to-Many
            modelBuilder.Entity<Manager>()
                .HasMany(m => m.Rentals)
                .WithOne(r => r.Manager)
                .HasForeignKey(r => r.ManagerId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict deletion if Manager has rentals

            base.OnModelCreating(modelBuilder);
        }
    }
}
