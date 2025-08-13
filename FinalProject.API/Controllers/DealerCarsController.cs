using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProject.BL;
using FinalProject.BL.DTO;

namespace FinalProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DealerCarsController : ControllerBase
    {
        private readonly IDealerCarBL _dealerCarBL;

        public DealerCarsController(IDealerCarBL dealerCarBL)
        {
            _dealerCarBL = dealerCarBL;
        }

        // GET: api/<DealerCarsController>
        [HttpGet]
        public IEnumerable<DealerCarDTO> Get()
        {
            var dealerCars = _dealerCarBL.GetAllDealerCars();
            return dealerCars;
        }

        // GET api/<DealerCarsController>/5
        [HttpGet("{id}")]
        public ActionResult<DealerCarDTO> Get(string id)
        {
            var dealerCar = _dealerCarBL.GetDealerCarById(id);
            if (dealerCar == null)
            {
                return NotFound($"DealerCar with ID {id} not found.");
            }
            return Ok(dealerCar);
        }

        // POST api/<DealerCarsController>
        [HttpPost]
        public ActionResult<DealerCarDTO> Post(DealerCarInsertDTO dealerCarInsertDTO)
        {
            try
            {
                if (dealerCarInsertDTO == null)
                {
                    throw new ArgumentNullException(nameof(dealerCarInsertDTO), "DealerCarInsertDTO cannot be null.");
                }
                var addedDealerCar = _dealerCarBL.AddDealerCar(dealerCarInsertDTO);
                if (addedDealerCar == null)
                {
                    return BadRequest("Failed to add the dealer car.");
                }
                return CreatedAtAction(nameof(Get), new { id = addedDealerCar.DealerCarId }, addedDealerCar);
            }
            catch (ArgumentException aEx)
            {
                return BadRequest($"Error adding dealer car: {aEx.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error adding dealer car: {ex.Message}");
            }
        }

        // PUT api/<DealerCarsController>/5
        [HttpPut("{id}")]
        public ActionResult<DealerCarDTO> Put(string id, DealerCarUpdateDTO dealerCarUpdateDTO)
        {
            try
            {
                var updateCar = _dealerCarBL.GetDealerCarById(id);
                if (updateCar == null)
                {
                    return NotFound($"DealerCar with ID {id} not found.");
                }
                dealerCarUpdateDTO.DealerCarId = id; // Ensure the ID is set for the update
                var updatedDealerCar = _dealerCarBL.UpdateDealerCar(dealerCarUpdateDTO);
                if (updatedDealerCar == null)
                {
                    return BadRequest("Failed to update the dealer car.");
                }
                return Ok(updatedDealerCar);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating dealer car with ID {id}: {ex.Message}");
            }
        }

        // DELETE api/<DealerCarsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                _dealerCarBL.DeleteDealerCar(id);
                return Ok($"Dealer car with ID {id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting dealer car with ID {id}: {ex.Message}");
            }
        }
    }
}
