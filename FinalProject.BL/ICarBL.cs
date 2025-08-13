using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalProject.BL.DTO;
using FinalProject.BO;

namespace FinalProject.BL
{
    public interface ICarBL
    {
        IEnumerable<CarDTO> GetCars();
        CarDTO GetById(string carId);
        CarDTO AddCar(CarInsertDTO carInsertDTO);
        CarDTO UpdateCar(CarUpdateDTO carUpdateDTO);
        void DeleteCar(string carId);
        IEnumerable<CarDTO> SearchCars(string search);
    }
}
