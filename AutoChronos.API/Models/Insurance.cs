namespace AutoChronos.API.Models
{
    public class Insurance
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public required string InsuranceCompany { get; set; }
        public int CarId { get; set; }
        public required Car Car { get; set; }
    }

}
