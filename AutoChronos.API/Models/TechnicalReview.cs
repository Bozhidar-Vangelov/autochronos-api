namespace AutoChronos.API.Models
{
    public class TechnicalReview
    {
        public int Id { get; set; }
        public DateTime ReviewDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CarId { get; set; }
        public Car? Car { get; set; }
    }
}
