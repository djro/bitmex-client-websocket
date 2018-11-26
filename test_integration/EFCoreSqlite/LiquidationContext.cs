using Microsoft.EntityFrameworkCore;

namespace EFCoreSqlite
{
    public class LiquidationContext: DbContext
    {
        public DbSet<Liquidation> Liquidations {get; set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlite("Filename=./liquidation.db");
        }
    }
}
