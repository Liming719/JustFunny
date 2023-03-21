using JustFunny.Models;
using Microsoft.EntityFrameworkCore;

namespace JustFunny.Database
{
    public class Fun_DbContext : DbContext
    {
        public Fun_DbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions{ get; set; }

    }
}
