using DiveDeep.Models;
using Microsoft.EntityFrameworkCore;
using DiveDeep.Persistence;

namespace DiveDeep.Data
{
    public class DiveDeepContext : DbContext
    {
        public DbSet<BCD> BCDs { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<DiveSuit> DiveSuits { get; set; }
        public DbSet<Fins> Fins { get; set; }
        public DbSet<MaskSnorkel> MaskSnorkels { get; set; }
        public DbSet<RegulatorSet> RegulatorSets { get; set; }
        public DbSet<Tank> Tanks { get; set; }
        public DbSet<Product> Products { get; set; }


        public DiveDeepContext(DbContextOptions<DiveDeepContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasOne<Product>(b => b.Product)
                .WithMany(b => b.Product)
                .HasForeignKey(p => p.ProductId);
        }
    }
}
