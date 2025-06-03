using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoffeeManagement.Model
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<Role> roles { get; set; }
        public DbSet<Shift> shifts { get; set; }
        public DbSet<Account> accounts { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> orderDetail { get; set; }
        public DbSet<Beverage> beverages { get; set; }
        public DbSet<Material> materials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
      

            // use for one -many khong dung cho 2 chieu
            //Convert RoleType in the Database
            modelBuilder.Entity<Account>().Property(a => a.RoleId)
               .HasConversion(new EnumToStringConverter<Role.RoleType>());

            modelBuilder.Entity<Role>().Property(r => r.Id)
            .HasConversion(new EnumToStringConverter<Role.RoleType>());

            modelBuilder.Entity<Account>(enity =>
            {
                enity.HasOne(a => a.Role)
                 .WithMany(r => r.Accounts)
                 .HasForeignKey(a => a.RoleId)
                 .OnDelete(DeleteBehavior.Restrict);

                enity.HasMany(r => r.Orders)
                .WithOne(a => a.Account)
                .HasForeignKey(o =>o.AccountId)
                .OnDelete(DeleteBehavior.Restrict);     
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasOne(od => od.Beverage)
                .WithMany(b => b.OrderDetails)
                .HasForeignKey(od => od.OrderDetailId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(od => od.Order) // chi vao object
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od =>od.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
            });



            
        }
    }
}
