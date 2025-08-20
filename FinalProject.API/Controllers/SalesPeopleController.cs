using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProject.BL;
using FinalProject.BL.DTO;


namespace FinalProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesPeopleController : ControllerBase
    {
        private readonly ISalesPersonBL _salesPersonBL;
        public SalesPeopleController(ISalesPersonBL salesPersonBL)
        {
            _salesPersonBL = salesPersonBL;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SalesPersonDTO>> GetAllSalesPeople()
        {
            try
            {
                var salesPeople = _salesPersonBL.GetAllSalesPeople();
                return Ok(salesPeople);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<SalesPersonDTO> GetSalesPersonById(string id)
        {
            try
            {
                var salesPerson = _salesPersonBL.GetById(id);
                if (salesPerson == null)
                {
                    return NotFound($"SalesPerson with ID {id} not found.");
                }
                return Ok(salesPerson);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving data: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<SalesPersonDTO> CreateSalesPerson( SalesPersonInsertDTO salesPersonInsertDTO)
        {
            if (salesPersonInsertDTO == null)
            {
                return BadRequest("SalesPerson data is null.");
            }
            try
            {
                var createdSalesPerson = _salesPersonBL.CreateSalesPerson(salesPersonInsertDTO);
                return CreatedAtAction(nameof(GetSalesPersonById), new { id = createdSalesPerson.SPId }, createdSalesPerson);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating SalesPerson: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<SalesPersonDTO> UpdateSalesPerson(string id, SalesPersonUpdateDTO salesPersonUpdateDTO)
        {
            try
            {
                if (salesPersonUpdateDTO == null || salesPersonUpdateDTO.SPId != id)
                {
                    return BadRequest("SalesPerson data is null or ID mismatch.");
                }
                var updatedSalesPerson = _salesPersonBL.UpdateSalesPerson(salesPersonUpdateDTO);
                if (updatedSalesPerson == null)
                {
                    return NotFound($"SalesPerson with ID {salesPersonUpdateDTO.SPId} not found.");
                }
                return Ok(updatedSalesPerson);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating SalesPerson: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteSalesPerson(string id)
        {
            try
            {
                _salesPersonBL.DeleteSalesPerson(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"SalesPerson with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting SalesPerson: {ex.Message}");
            }
        }
    }
}
