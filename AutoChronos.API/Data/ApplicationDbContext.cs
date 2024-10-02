using AutoChronos.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace AutoChronos.API.Data
{


    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<OilChange> OilChanges { get; set; }
        public DbSet<TechnicalReview> TechnicalReviews { get; set; }
        public DbSet<Vignette> Vignettes { get; set; }
    }

}
