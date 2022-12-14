using Microsoft.EntityFrameworkCore;

namespace RealEsateCRMAPI.Entities
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions options) : base(options)
        {
        }        
        public DbSet<Designation> Designations { get; set; }
    }
}
