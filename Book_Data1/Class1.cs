using Book_Domin;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Book_Data1
{
    public class BookDBcontext : DbContext
    {
        public DbSet<Book> books { get; set; }
        public DbSet<users> users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source= (localdb)\\MSSQLLocalDB; Initial Catalog=BookDb")
                          .LogTo(log => new customerlogger().log(log));
        }

        


    }
}