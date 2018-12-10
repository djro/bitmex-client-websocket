using Microsoft.EntityFrameworkCore;

namespace Bitmex.Client.Websocket.EFCoreSqlite
{
    public class BitmexDbContext: DbContext
    {
        public DbSet<Liquidation> Liquidations {get; set;}
        public DbSet<Trade> Trades {get;set;}
        public DbSet<BookLevel> BookLevels {get;set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trade>()
                .HasKey(c => new {c.Timestamp, c.Symbol});
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlite("Filename=./bitmex.db");
        }
    }
}
