using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProject.BL;
using FinalProject.BL.DTO;


namespace FinalProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestsController : ControllerBase
    {
        private readonly IGuestBL _guestBL;
        public GuestsController(IGuestBL guestBL)
        {
            _guestBL = guestBL;
        }

        [HttpGet]
        public ActionResult<IEnumerable<GuestDTO>> GetGuests()
        {
            try
            {
                var guests = _guestBL.GetGuests();
                return Ok(guests);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving guests: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<GuestDTO> GetById(int id)
        {
            try
            {
                var guest = _guestBL.GetById(id);
                return Ok(guest);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Guest with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error retrieving guest: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult<GuestDTO> Create(GuestInsertDTO guestInsertDTO)
        { 
            if (guestInsertDTO == null)
            {
                return BadRequest("Guest data is null.");
            }
            try
            {
                var createdGuest = _guestBL.AddGuest(guestInsertDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdGuest.GuestId }, createdGuest);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error creating guest: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<GuestDTO> Update(int id, GuestUpdateDTO guestUpdateDTO)
        {
            //if (guestUpdateDTO == null || guestUpdateDTO.GuestId != id)
            //{
            //    return BadRequest("Guest data is null.");
            //}
            try
            {
                var updatedGuest = _guestBL.UpdateGuest(guestUpdateDTO);
                return Ok(updatedGuest);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Guest with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating guest: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                _guestBL.DeleteGuest(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Guest with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error deleting guest: {ex.Message}");
            }
        }

    }
}
