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
                .HasOne(b => b.Product)
                .WithMany(p => p.Bookings)
                .HasForeignKey(b => b.ProductId);



            //modelBuilder.Entity<Product>(r =>
            //{
            //    r.HasData(InMemoryProductRepository.GetAll());
            //});

            modelBuilder.Entity<Product>(p =>
            {
                p.UseTptMappingStrategy();
                p.ToTable("Products");
            });
            modelBuilder.Entity<BCD>(p =>
            {
                p.ToTable("BCDs");
            });
            modelBuilder.Entity<DiveSuit>(p =>
            {
                p.ToTable("DiveSuits");
            });
            modelBuilder.Entity<Fins>(p =>
            {
                p.ToTable("Fins");
            });
            modelBuilder.Entity<MaskSnorkel>(p =>
            {
                p.ToTable("MAskSnorkels");
            });
            modelBuilder.Entity<RegulatorSet>(p =>
            {
                p.ToTable("RegulatorSets");
            });
            modelBuilder.Entity<Tank>(p =>
            {
                p.ToTable("Tanks");
            });


            base.OnModelCreating(modelBuilder);

        }


    }
}
