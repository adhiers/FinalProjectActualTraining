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
    }
}
