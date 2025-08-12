using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BL.DTO;
using FinalProject.BO;
using FinalProject.DAL;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FinalProject.BL
{
    public class CarBL : ICarBL
    {
        private readonly ICar _carDAL;
        public CarBL(ICar carDAL)
        {
            _carDAL = carDAL;
        }
        public CarDTO AddCar(CarInsertDTO carInsertDTO)
        {
            try
            { 
                var car = new Car
                {
                    CarId = carInsertDTO.CarId,
                    ModelType = carInsertDTO.ModelType,
                    FuelType = carInsertDTO.FuelType,
                    BasePrice = carInsertDTO.BasePrice,
                    Stock = carInsertDTO.Stock
                };

                var addedCar = _carDAL.Create(car);
                return new CarDTO
                {
                    CarId = addedCar.CarId,
                    ModelType = addedCar.ModelType,
                    FuelType = addedCar.FuelType,
                    BasePrice = addedCar.BasePrice,
                    Stock = addedCar.Stock
                };
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while adding the car.", ex);
            }
        }

        public void DeleteCar(string carId)
        {
            try
            {
                var car = _carDAL.GetById(carId);
                if (car == null)
                {
                    throw new Exception("Car not found");
                }
                _carDAL.Delete(carId);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while deleting the car.", ex);
            }
        }

        public CarDTO GetById(string carId)
        {
            var car = _carDAL.GetById(carId);
            if (car == null)
            {
                throw new Exception("Car not found");
            }
            return new CarDTO
            {
                CarId = car.CarId,
                ModelType = car.ModelType,
                FuelType = car.FuelType,
                BasePrice = car.BasePrice,
                Stock = car.Stock
            };
        }

        public IEnumerable<CarDTO> GetCars()
        {
            var carDTOs = new List<CarDTO>();
            var cars = _carDAL.GetAll();
            foreach (var car in cars)
            {
                carDTOs.Add(new CarDTO
                {
                    CarId = car.CarId,
                    ModelType = car.ModelType,
                    FuelType = car.FuelType,
                    BasePrice = car.BasePrice,
                    Stock = car.Stock
                });
            }
            return carDTOs;
        }

        public IEnumerable<CarDTO> SearchCars(string search)
        {
            var carDTOs = new List<CarDTO>();
            var cars = _carDAL.GetBySearch(search);
            foreach (var car in cars)
            {
                carDTOs.Add(new CarDTO
                {
                    CarId = car.CarId,
                    ModelType = car.ModelType,
                    FuelType = car.FuelType,
                    BasePrice = car.BasePrice,
                    Stock = car.Stock
                });
            }
            return carDTOs;
        }

        public CarDTO UpdateCar(CarUpdateDTO carUpdateDTO)
        {
            try
            {
                var car = _carDAL.GetById(carUpdateDTO.CarId);
                if (car == null)
                {
                    throw new Exception("Car not found");
                }
                car.ModelType = carUpdateDTO.ModelType;
                car.FuelType = carUpdateDTO.FuelType;
                car.BasePrice = carUpdateDTO.BasePrice;
                car.Stock = carUpdateDTO.Stock;
                var updatedCar = _carDAL.Update(car);

                return new CarDTO
                {
                    CarId = updatedCar.CarId,
                    ModelType = updatedCar.ModelType,
                    FuelType = updatedCar.FuelType,
                    BasePrice = updatedCar.BasePrice,
                    Stock = updatedCar.Stock
                };
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                throw new Exception("An error occurred while updating the car.", ex);
            }
        }
    }
}
