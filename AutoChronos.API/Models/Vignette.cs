using System.ComponentModel.DataAnnotations;

namespace AutoChronos.API.Models
{
    public class Vignette
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string? VignetteType { get; set; } 
        public int CarId { get; set; }
        public Car? Car { get; set; }
    }
}
