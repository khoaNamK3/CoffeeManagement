using Microsoft.EntityFrameworkCore;

namespace CoffeeManagement.Model
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) { }

        public DbSet<Role> roles { get; set; }
        public DbSet<Shift> shifts { get; set; }
        public DbSet<Account> accounts { get; set; }
        public DbSet<AccountShift> accountShifts { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> orderDetail { get; set; }
        public DbSet<BeveragesOrderDetail> beveragesOrderDetail { get; set; }
        public DbSet<BeverageMaterial> beverageMaterials { get; set; }
        public DbSet<Beverage> beverages { get; set; }
        public DbSet<Material> materials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define composite key for AccountShift (Many-to-Many between Account and Shift)
            modelBuilder.Entity<AccountShift>()
                .HasKey(asr => new { asr.AccountId, asr.ShiftId });

            modelBuilder.Entity<AccountShift>()
                .HasOne(asr => asr.Account)
                .WithMany(a => a.AccountShifts)
                .HasForeignKey(asr => asr.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AccountShift>()
                .HasOne(asr => asr.Shift)
                .WithMany(s => s.AccountShifts)
                .HasForeignKey(asr => asr.ShiftId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define composite key for BeveragesOrderDetail (Many-to-Many between Beverage and OrderDetail)
            modelBuilder.Entity<BeveragesOrderDetail>()
                .HasKey(bod => new { bod.OrderDetailId, bod.BeverageId });

            modelBuilder.Entity<BeveragesOrderDetail>()
                .HasOne(bod => bod.OrderDetail)
                .WithMany(od => od.BeveragesOrders)
                .HasForeignKey(bod => bod.OrderDetailId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BeveragesOrderDetail>()
              .HasOne(bod => bod.Beverage)
            .WithMany(b => b.BeverageOrderDetails) // Đúng tên thuộc tính
             .HasForeignKey(bod => bod.BeverageId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define composite key for BeverageMaterial (Many-to-Many between Beverage and Material)
            modelBuilder.Entity<BeverageMaterial>()
                .HasKey(bm => new { bm.BeverageId, bm.MaterialId });

            modelBuilder.Entity<BeverageMaterial>()
                .HasOne(bm => bm.Beverage)
                .WithMany(b => b.BeverageMaterials)
                .HasForeignKey(bm => bm.BeverageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<BeverageMaterial>()
                .HasOne(bm => bm.Material)
                .WithMany(m => m.BeverageMaterials)
                .HasForeignKey(bm => bm.MaterialId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
