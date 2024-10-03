namespace AutoChronos.API.Models
{
    public class Car
    {
        public int Id { get; set; }
        public required string Manufacturer { get; set; }
        public required string Model { get; set; }
        public int CurrentKilometers { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }

        public ICollection<Insurance>? Insurances { get; set; }
        public ICollection<TechnicalReview>? TechnicalReviews { get; set; }
        public ICollection<Vignette>? Vignettes { get; set; }
        public ICollection<OilChange>? OilChanges { get; set; }
    }
}
