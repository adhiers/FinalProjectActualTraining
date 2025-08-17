using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProject.BL;
using FinalProject.BL.DTO;

namespace FinalProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulingsController : ControllerBase
    {
        private readonly ISchedulingBL _schedulingBL;
        public SchedulingsController(ISchedulingBL schedulingBL)
        {
            _schedulingBL = schedulingBL;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SchedulingDTO>> GetAllSchedulings()
        {
            try
            {
                var schedulings = _schedulingBL.GetSchedulings();
                return Ok(schedulings);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving schedules: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<SchedulingDTO> GetSchedulingById(int id)
        {
            try
            {
                var scheduling = _schedulingBL.GetById(id);
                return Ok(scheduling);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Schedule with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving schedule: {ex.Message}");
            }
        }

        [HttpGet("search/{search}")]
        public ActionResult<IEnumerable<SchedulingDTO>> GetSchedulingsBySearch(string search)
        {
            try
            {
                var schedulings = _schedulingBL.GetBySearch(search);
                if (schedulings == null || !schedulings.Any())
                {
                    return NotFound($"No schedules found matching search term '{search}'.");
                }
                return Ok(schedulings);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error searching schedules: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<SchedulingDTO> Post(SchedulingInsertDTO schedulingInsertDTO)
        {
            try
            {
                var createdScheduling = _schedulingBL.AddScheduling(schedulingInsertDTO);
                return CreatedAtAction(nameof(GetSchedulingById), new { id = createdScheduling.ScheduleId }, createdScheduling);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating schedule: {ex.Message}");
            }
        }

        [HttpPut]
        public ActionResult<SchedulingDTO> Put(SchedulingUpdateDTO schedulingUpdateDTO)
        {
            try
            {
                var updatedScheduling = _schedulingBL.UpdateScheduling(schedulingUpdateDTO);
                return Ok(updatedScheduling);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Schedule with ID {schedulingUpdateDTO.ScheduleId} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating schedule: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _schedulingBL.DeleteScheduling(id);
                return Ok($"Schedule with ID {id} deleted successfully");
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Schedule with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting schedule: {ex.Message}");
            }
        }
    }
}
