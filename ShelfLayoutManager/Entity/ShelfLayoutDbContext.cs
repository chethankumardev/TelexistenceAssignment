using Microsoft.EntityFrameworkCore;

namespace ShelfLayoutManager.Entity
{
    /// <summary>
    /// Represents the database context for the ShelfLayoutManager application.
    /// </summary>
    public class ShelfLayoutDbContext : DbContext
    {
        public ShelfLayoutDbContext(DbContextOptions<ShelfLayoutDbContext> options) : base(options)
        {
        }
        public ShelfLayoutDbContext() 
        {
        }


        public DbSet<Cabinet> Cabinets { get; set; }
        public DbSet<Row> Rows { get; set; }
        public DbSet<Lane> Lanes { get; set; }
        public DbSet<SKU> SKUs { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships between entities
            modelBuilder.Entity<Row>()
                .HasOne(row => row.Cabinet)
                .WithMany(cabinet => cabinet.Rows)
                .HasForeignKey(row => row.CabinetId);

            modelBuilder.Entity<Lane>()
                .HasOne(lane => lane.Row)
                .WithMany(row => row.Lanes)
                .HasForeignKey(lane => lane.RowId);
        }
    }
}
