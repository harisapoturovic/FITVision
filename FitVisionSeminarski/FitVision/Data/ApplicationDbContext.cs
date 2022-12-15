using FitVision.Modul2.Models;
using Microsoft.EntityFrameworkCore;

namespace FitVision.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Animal>  Animal { get; set; }

    }
}
