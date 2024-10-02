namespace AutoChronos.API.Dtos
{
    public class InsuranceDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public required string InsuranceCompany { get; set; }
    }

}
