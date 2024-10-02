namespace AutoChronos.API.Models
{
    public class OilChange
    {
        public int Id { get; set; }
        public DateTime ChangeDate { get; set; }
        public int CurrentKilometers { get; set; }
        public List<string>? FiltersChanged { get; set; }
        public int CarId { get; set; }
        public Car? Car { get; set; }
    }

}
