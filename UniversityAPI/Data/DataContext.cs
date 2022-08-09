using Microsoft.EntityFrameworkCore;
using UniversityAPI.Models;

namespace UniversityAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<University> Universities { get; set; }

   
        public DbSet<Faculty> Faculty { get; set; }

        public DbSet<Career> Careers { get; set; }

        public DbSet<Comment> Comment { get; set; }
    }
}
