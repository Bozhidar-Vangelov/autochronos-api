﻿namespace AutoChronos.API.Dtos
{
    public class RegisterDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }

}
