using FinalProject.BL;
using FinalProject.BL.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProject.BO;

namespace FinalProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarBL _carBL;
        public CarsController(ICarBL carBL)
        {
            _carBL = carBL;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CarDTO>> GetCars()
        {
            var cars = _carBL.GetCars();
            if (cars == null || !cars.Any())
            {
                return NotFound("No cars found.");
            }
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public ActionResult<CarDTO> GetCarById(string id)
        {
            var car = _carBL.GetById(id);
            if (car == null)
            {
                return NotFound($"Car with ID {id} not found.");
            }
            return Ok(car);
        }

        [HttpGet("BySearch")]
        public ActionResult<IEnumerable<CarDTO>> GetCarsBySearch(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return BadRequest("Search term cannot be null or empty.");
            }

            var cars = _carBL.SearchCars(search);
            return Ok(cars);
        }

        [HttpPost]
        public ActionResult<CarDTO> AddCar(CarInsertDTO carInsertDto)
        {

            try
            {
                if (carInsertDto == null)
                {
                    return BadRequest("Car data cannot be null.");
                }
                var addedCar = _carBL.AddCar(carInsertDto);
                return CreatedAtAction(nameof(GetCarById), new { id = addedCar.CarId }, addedCar);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error adding car: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<CarDTO> UpdateCar(string id, CarUpdateDTO carUpdateDto)
        {
            if (carUpdateDto == null || id != carUpdateDto.CarId)
            {
                return BadRequest("Car data is invalid.");
            }

            try
            {
                var updatedCar = _carBL.UpdateCar(carUpdateDto);
                return Ok(updatedCar);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating car: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCar(string id)
        {
            try
            {
                _carBL.DeleteCar(id);
                return Ok($"Car id: {id} deleted successfully.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}