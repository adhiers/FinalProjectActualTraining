using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FinalProject.BL;
using FinalProject.BL.DTO;

namespace FinalProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDrivesController : ControllerBase
    {
        private readonly ITestDriveBL _testDriveBL;
        public TestDrivesController(ITestDriveBL testDriveBL)
        {
            _testDriveBL = testDriveBL;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TestDriveDTO>> GetAll()
        {
            try
            {
                var testDrives = _testDriveBL.GetTestDrives();
                return Ok(testDrives);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving test drives.");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<TestDriveDTO> GetById(string id)
        {
            try
            {
                var testDrive = _testDriveBL.GetById(id);
                if (testDrive == null)
                {
                    return NotFound($"Test drive with ID {id} not found.");
                }
                return Ok(testDrive);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Test drive with ID {id} not found.");
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the test drive.");
            }
        }

        [HttpPost]
        public ActionResult<TestDriveDTO> Create(TestDriveInsertDTO testDriveInsertDTO)
        {
            try
            {
                if (testDriveInsertDTO == null)
                {
                    return BadRequest("Test drive insert DTO cannot be null.");
                }
                var createdTestDrive = _testDriveBL.AddTestDrive(testDriveInsertDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdTestDrive.Tdid }, createdTestDrive);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the test drive.");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<TestDriveDTO> Update(string id, TestDriveUpdateDTO testDriveUpdateDTO)
        {
            try
            {
                if (testDriveUpdateDTO == null || testDriveUpdateDTO.Tdid != id)
                {
                    return BadRequest("Test drive DTO cannot be null and ID must match.");
                }
                var updatedTestDrive = _testDriveBL.UpdateTestDrive(testDriveUpdateDTO);
                return Ok(updatedTestDrive);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Test drive with ID {id} not found.");
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the test drive.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                _testDriveBL.DeleteTestDrive(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Test drive with ID {id} not found.");
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the test drive.");
            }
        }
    }
}
