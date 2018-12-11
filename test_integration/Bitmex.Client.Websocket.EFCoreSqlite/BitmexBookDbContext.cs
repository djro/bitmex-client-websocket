using Microsoft.EntityFrameworkCore;

namespace Bitmex.Client.Websocket.EFCoreSqlite
{
    public class BitmexBookDbContext: DbContext
    {
        public DbSet<BookLevel> BookLevels {get;set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            optionsBuilder.UseSqlite("Filename=./bitmex-book.db");
        }
    }
}
