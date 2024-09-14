﻿namespace AutoChronos.API.Models
{
    public class Car
    {
        public int Id { get; set; } // Primary Key
        public required string Manufacturer { get; set; }
        public required string Model { get; set; }
        public int CurrentKilometers { get; set; }
    }

}
