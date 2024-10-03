using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AutoChronos.API.Models;
using Microsoft.AspNetCore.Authorization;
using AutoChronos.API.Services;

namespace AutoChronos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/cars")]
    public class CarsController(CarsService carsService) : ControllerBase
    {

        private string? GetUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var cars = await carsService.GetAllCarsAsync(userId);  
            var carDtos = cars.Select(c => carsService.MapCarToDto(c)).ToList();

            return Ok(carDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var car = await carsService.GetCarByIdAsync(id, userId);

            if (car == null)
            {
                return NotFound();
            }

            var carDto = carsService.MapCarToDto(car);
            return Ok(carDto);
        }

        [HttpPost]
        public async Task<ActionResult<Car>> AddCar(Car car)
        {
            var userId = GetUserId();
            car.UserId = userId;

            var createdCar = await carsService.CreateCarAsync(car);
            var createdCarDto = carsService.MapCarToDto(createdCar);
            return CreatedAtAction(nameof(GetCar), new { id = createdCar.Id }, createdCarDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, Car car)
        {
            var userId = GetUserId();

            if (id != car.Id || car.UserId != userId)
            {
                return BadRequest("Car ID mismatch.");
            }

            car.UserId = userId;

            var updatedCar = await carsService.UpdateCarAsync(car);

            if (updatedCar == null)
            {
                return NotFound("Car not found.");
            }

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authenticated.");
            }

            var deletedCar = await carsService.DeleteCarAsync(id, userId);

            if (!deletedCar)
            {
                return NotFound("Car not found.");
            }

            return NoContent();
        }
    }
}
