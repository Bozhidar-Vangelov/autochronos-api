using AutoChronos.API.Models;
using Microsoft.EntityFrameworkCore;


namespace AutoChronos.API.Data
{


    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        // Define DbSet properties for each entity
        public DbSet<Car> Cars { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<OilChange> OilChanges { get; set; }
    }

}
