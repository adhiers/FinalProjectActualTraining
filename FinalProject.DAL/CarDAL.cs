using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BO;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.DAL
{
    public class CarDAL : ICar
    {
        private readonly FinalProjectDBContext _context;
        public CarDAL(FinalProjectDBContext context)
        {
            _context = context;
        }
        public Car Create(Car item)
        {
            try
            {
                _context.Cars.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while creating the car.", ex);
            }
        }

        public void Delete(string id)
        {
            var car = GetById(id);
            if (car == null)
            {
                throw new Exception("Car not found");
            }
            try
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while deleting the car.", ex);
            }
        }

        public IEnumerable<Car> GetAll()
        {
            var cars = from c in _context.Cars orderby c.CarId ascending select c;
            return cars;
        }

        public Car GetById(string id)
        {
            var result = _context.Cars.Where(c => c.CarId == id).FirstOrDefault();
            if(result == null)
            {
                throw new KeyNotFoundException($"Car with ID {id} not found.");
            }
            return result;
        }

        public IEnumerable<Car> GetBySearch(string search)
        {
            try
            {
                var results = _context.Cars.Where(c => c.ModelType.Contains(search) || c.FuelType.Contains(search))
                    .ToList();
                return results;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while searching for cars.", ex);
            }
        }

        public Car Update(Car item)
        {
            var result = GetById(item.CarId);
            if(result == null)
            {
                throw new KeyNotFoundException($"Car with ID {item.CarId} not found.");
            }
            try
            {
                _context.Entry(result).CurrentValues.SetValues(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while updating the car.", ex);
            }
        }
    }
}
