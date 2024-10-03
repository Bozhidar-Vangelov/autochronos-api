namespace AutoChronos.API.Dtos
{
    public class OilChangeDto
    {
        public int Id { get; set; }
        public DateTime ChangeDate { get; set; }
        public int CurrentKilometers { get; set; }
        public List<string>? FiltersChanged { get; set; }
    }
}
