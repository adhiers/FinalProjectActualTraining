using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProject.BL;
using FinalProject.BL.DTO;

namespace FinalProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationsController : ControllerBase
    {
        private readonly IConsultationBL _consultationBL;
        public ConsultationsController(IConsultationBL consultationBL)
        {
            _consultationBL = consultationBL;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ConsultationDTO>> GetAll()
        {
            try
            {
                var consultations = _consultationBL.GetConsultations();
                return Ok(consultations);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving consultations: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ConsultationDTO> GetById(string id)
        {
            try
            {
                var consultation = _consultationBL.GetById(id);
                if (consultation == null)
                {
                    return NotFound($"Consultation with ID {id} not found.");
                }
                return Ok(consultation);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving consultation: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<ConsultationDTO> Create(ConsultationInsertDTO consultationInsertDTO)
        {
            try
            {
                var createdConsultation = _consultationBL.AddConsultation(consultationInsertDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdConsultation.ConsultId }, createdConsultation);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating consultation: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<ConsultationDTO> Update(string id, ConsultationUpdateDTO consultationUpdateDTO)
        {
            if (id != consultationUpdateDTO.ConsultId)
            {
                return BadRequest("Consultation ID mismatch.");
            }
            try
            {
                var updatedConsultation = _consultationBL.UpdateConsultation(consultationUpdateDTO);
                return Ok(updatedConsultation);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Consultation with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating consultation: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                _consultationBL.DeleteConsultation(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Consultation with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting consultation: {ex.Message}");
            }
        }
    }
}
