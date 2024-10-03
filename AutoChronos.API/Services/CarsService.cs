using AutoChronos.API.Data;
using AutoChronos.API.Dtos;
using AutoChronos.API.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoChronos.API.Services
{
    public class CarsService(ApplicationDbContext context)
    {
        public async Task<IEnumerable<Car>> GetAllCarsAsync(string userId)
        {
            return await context.Cars
                .Include(c => c.Insurances)
                .Include(c => c.TechnicalReviews)
                .Include(c => c.Vignettes)
                .Include(c => c.OilChanges)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<Car?> GetCarByIdAsync(int carId, string userId)
        {
            return await context.Cars
                .Include(c => c.Insurances)
                .Include(c => c.TechnicalReviews)
                .Include(c => c.Vignettes)
                .Include(c => c.OilChanges)
                .FirstOrDefaultAsync(c => c.Id == carId && c.UserId == userId);
        }

        public async Task<Car> CreateCarAsync(Car car)
        {
            context.Cars.Add(car);

            if (car.Insurances != null && car.Insurances.Count > 0)
            {
                foreach (var insurance in car.Insurances)
                {
                    insurance.CarId = car.Id;
                    context.Insurances.Add(insurance);
                }
            }

            if (car.TechnicalReviews != null && car.TechnicalReviews.Count > 0)
            {
                foreach (var review in car.TechnicalReviews)
                {
                    review.CarId = car.Id;
                    context.TechnicalReviews.Add(review);
                }
            }

            if (car.Vignettes != null && car.Vignettes.Count > 0)
            {
                foreach (var vignette in car.Vignettes)
                {
                    vignette.CarId = car.Id;
                    context.Vignettes.Add(vignette);
                }
            }

            await context.SaveChangesAsync();
            return car;
        }


        public async Task<Car?> UpdateCarAsync(Car car)
        {
            var existingCar = await context.Cars
                .Include(c => c.Insurances)
                .Include(c => c.TechnicalReviews)
                .Include(c => c.Vignettes)
                .FirstOrDefaultAsync(c => c.Id == car.Id && c.UserId == car.UserId);

            if (existingCar == null)
            {
                return null;
            }

            existingCar.Manufacturer = car.Manufacturer;
            existingCar.Model = car.Model;
            existingCar.CurrentKilometers = car.CurrentKilometers;

            if (car.Insurances != null && car.Insurances.Count > 0)
            {
                context.Insurances.RemoveRange(existingCar.Insurances!);
                foreach (var insurance in car.Insurances)
                {
                    insurance.CarId = existingCar.Id;
                    context.Insurances.Add(insurance);
                }
            }

            if (car.TechnicalReviews != null && car.TechnicalReviews.Count > 0)
            {
                context.TechnicalReviews.RemoveRange(existingCar.TechnicalReviews!);
                foreach (var review in car.TechnicalReviews)
                {
                    review.CarId = existingCar.Id;
                    context.TechnicalReviews.Add(review);
                }
            }

            if (car.Vignettes != null && car.Vignettes.Count > 0)
            {
                context.Vignettes.RemoveRange(existingCar.Vignettes!);
                foreach (var vignette in car.Vignettes)
                {
                    vignette.CarId = existingCar.Id;
                    context.Vignettes.Add(vignette);
                }
            }

            if (car.OilChanges != null && car.OilChanges.Count > 0)
            {
                foreach (var oilChange in car.OilChanges)
                {
                    oilChange.CarId = car.Id;
                    context.OilChanges.Add(oilChange);
                }
            }

            await context.SaveChangesAsync();
            return existingCar;
        }

        public async Task<bool> DeleteCarAsync(int carId, string userId)
        {
            var car = await context.Cars
                .Include(c => c.Insurances)
                .Include(c => c.TechnicalReviews)
                .Include(c => c.Vignettes)
                .Include(c => c.OilChanges)
                .FirstOrDefaultAsync(c => c.Id == carId && c.UserId == userId);

            if (car == null)
            {
                return false;
            }

            context.Cars.Remove(car);
            await context.SaveChangesAsync();
            return true;
        }

        public CarDto MapCarToDto(Car car)
        {
            return new CarDto
            {
                Id = car.Id,
                Manufacturer = car.Manufacturer,
                Model = car.Model,
                CurrentKilometers = car.CurrentKilometers,
                Insurances = car.Insurances?.Select(i => new InsuranceDto
                {
                    Id = i.Id,
                    StartDate = i.StartDate,
                    ExpirationDate = i.ExpirationDate,
                    InsuranceCompany = i.InsuranceCompany
                }).ToList(),
                TechnicalReviews = car.TechnicalReviews?.Select(tr => new TechnicalReviewDto
                {
                    Id = tr.Id,
                    ReviewDate = tr.ReviewDate,
                    ExpirationDate = tr.ExpirationDate
                }).ToList(),
                Vignettes = car.Vignettes?.Select(v => new VignetteDto
                {
                    Id = v.Id,
                    StartDate = v.StartDate,
                    ExpirationDate = v.ExpirationDate,
                    VignetteType = v.VignetteType
                }).ToList(),
                OilChanges = car.OilChanges?.Select(oc => new OilChangeDto
                {
                    Id = oc.Id,
                    ChangeDate = oc.ChangeDate,
                    CurrentKilometers = oc.CurrentKilometers,
                    FiltersChanged = oc.FiltersChanged
                }).ToList()
            };
        }

    }
}
