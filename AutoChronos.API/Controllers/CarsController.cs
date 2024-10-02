using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AutoChronos.API.Data;
using AutoChronos.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace AutoChronos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/cars")]
    public class CarsController(ApplicationDbContext context) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await context.Cars.Where(c => c.UserId == userId).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var car = await context.Cars.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (car == null)
            {
                return NotFound();
            }

            return car;
        }

        [HttpPost]
        public async Task<ActionResult<Car>> AddCar(Car car)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized();
            }

            car.UserId = userId;

            context.Cars.Add(car);
            await context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCar), new { id = car.Id }, car);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCar(int id, Car car)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id != car.Id || car.UserId != userId)
            {
                return BadRequest();
            }

            context.Entry(car).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(id, userId!))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var car = await context.Cars.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);

            if (car == null)
            {
                return NotFound();
            }

            context.Cars.Remove(car);
            await context.SaveChangesAsync();

            return NoContent();
        }
        private bool CarExists(int id, string userId)
        {
            return context.Cars.Any(e => e.Id == id && e.UserId == userId);
        }
    }
}
