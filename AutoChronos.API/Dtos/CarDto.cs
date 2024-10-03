namespace AutoChronos.API.Dtos
{
    public class CarDto
    {
        public int Id { get; set; }
        public required string Manufacturer { get; set; }
        public required string Model { get; set; }
        public int CurrentKilometers { get; set; }
        public List<InsuranceDto>? Insurances { get; set; }
        public List<TechnicalReviewDto>? TechnicalReviews { get; set; }
        public List<VignetteDto>? Vignettes { get; set; }
        public List<OilChangeDto>? OilChanges { get; set; }
    }

}
