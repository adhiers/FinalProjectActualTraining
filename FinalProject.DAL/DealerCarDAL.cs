using FinalProject.BO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.DAL
{
    public class DealerCarDAL : IDealerCar
    {
        private readonly FinalProjectDBContext _context;
        public DealerCarDAL(FinalProjectDBContext context)
        {
            _context = context;
        }

        private bool IsExistDealerAndCar(string carId, string dealerId)
        {
            return _context.DealerCars.Any(dc => dc.CarId == carId && dc.DealerId == dealerId);
        }

        public DealerCar Create(DealerCar item)
        {
            try
            {
                var existingCarAndDealer = IsExistDealerAndCar(item.CarId, item.DealerId);

                if (existingCarAndDealer)
                {
                    throw new ArgumentException($"A dealer car with CarId {item.CarId} and DealerId {item.DealerId} already exists.");
                }

                _context.DealerCars.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (ArgumentException aEx)
            {
                // Log the exception (not implemented here)
                throw new ArgumentException($"{aEx.Message}");
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while adding the dealer car.", ex);
            }
        }

        public void Delete(string id)
        {
            try
            {
                var entity = GetById(id);
                if (entity == null)
                {
                    throw new Exception("Dealer car not found");
                }
                _context.DealerCars.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting dealer car: {ex.Message}");
            }
        }

        public IEnumerable<DealerCar> GetAll()
        {
            var results = _context.DealerCars.Include(c => c.Car).Include(d => d.Dealer).AsNoTracking()
                                              .ToList();
            return results;
        }

        public DealerCar GetById(string id)
        {
            var result = _context.DealerCars.Include(c => c.Car).Include(d => d.Dealer)
                                        .FirstOrDefault(dc => dc.DealerCarId == id);

            if (result == null) throw new Exception($"DealerCar with ID {id} not found.");

            return result;
        }

        public DealerCar Update(DealerCar item)
        {
            try
            {
                var existingDealerCar = GetById(item.DealerCarId);
                if (existingDealerCar == null)
                {
                    throw new ArgumentException($"DealerCar with ID {item.DealerCarId} not found.");
                }
                else
                {
                    if (existingDealerCar.CarId != item.CarId || existingDealerCar.DealerId != item.DealerId)
                    {
                        var existingCarAndDealer = IsExistDealerAndCar(item.CarId, item.DealerId);
                        if (existingCarAndDealer)
                        {
                            throw new ArgumentException($"A dealer car with CarId {item.CarId} and DealerId {item.DealerId} already exists.");
                        }
                    }
                }


                existingDealerCar.CarId = item.CarId;
                existingDealerCar.DealerId = item.DealerId;
                existingDealerCar.DealerCarPrice = item.DealerCarPrice;
                
                _context.SaveChanges();
                return existingDealerCar;
            }
            catch (ArgumentException aEx)
            {
                // Log the exception (not implemented here)
                throw new ArgumentException($"{aEx.Message}");
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                throw new Exception("An error occurred while updating the dealer car.", ex);
            }
        }
    }
}
