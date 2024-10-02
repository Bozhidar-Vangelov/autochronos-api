namespace AutoChronos.API.Dtos
{
    public class VignetteDto
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? VignetteType { get; set; }
    }
}
