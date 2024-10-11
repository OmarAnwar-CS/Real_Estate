using _DataAccess.Models;
using API_Project.DataAccess.Models;
using application.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;



namespace application.DataAccess
{



    public class AppDbContext : DbContext
    {
        public DbSet<Property> Properties { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Inquiry> Inquiries { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Amenities> Amenities { get; set; }
        public DbSet<PropertyImage> Images { get; set; }
        public DbSet<City> Cities { get; set; }



        //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //            => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Real_Estate_Db_V3.9;Integrated Security=True;TrustServerCertificate=True");
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Data Source=DESKTOP-5JC2MA4\\SQLEXPRESS;Initial Catalog=Real_Estate_Db_V2;Integrated Security=True;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // العلاقة بين Property و Owner
            modelBuilder.Entity<Property>()
                .HasOne(p => p.Owner)
                .WithMany(u => u.Properties)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            // العلاقة بين Property و Amenities
            modelBuilder.Entity<Property>()
                .HasOne(p => p.Amenities)
                .WithMany(a => a.Properties)
                .HasForeignKey(p => p.AmenitiesId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Property>()
                .HasMany(p => p.Images)
                .WithOne(a => a.Property)
                .HasForeignKey(p => p.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.City)
                .WithMany(a => a.Properties)
                .HasForeignKey(p => p.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<City>()
            .HasIndex(c => c.Name)
            .IsUnique();

            // العلاقات الأخرى
            modelBuilder.Entity<Inquiry>()
                .HasOne(i => i.Property)
                .WithMany(p => p.Inquiries)
                .HasForeignKey(i => i.PropertyId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Inquiry>()
                .HasOne(i => i.User)
                .WithMany(u => u.Inquiries)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // تأكد من صحة العلاقة بين User و العقارات
            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.FullName)
                .HasComputedColumnSql("[F_Name] + ' ' + [L_Name]");
        }


        public override int SaveChanges()
        {

            var Entities = from e in ChangeTracker.Entries()
                           where e.State == EntityState.Modified ||
                           e.State == EntityState.Added
                           select e.Entity;

            foreach (var Entity in Entities)
            {
                ValidationContext validationContext = new ValidationContext(Entity);
                Validator.ValidateObject(Entity, validationContext, true);
            }

            return base.SaveChanges();
        }
    }

}
