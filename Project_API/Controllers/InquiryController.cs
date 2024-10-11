using _Services.Contracts;
using _Services.Models.Inquiry;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquiryController : ControllerBase
    {
        private readonly IInquiryService _inquiryService;

        public InquiryController(IInquiryService inquiryService)
        {
            _inquiryService = inquiryService;
        }

        // Get all inquiries
        [HttpGet]
        public IActionResult GetInquiries()
        {
            var inquiries = _inquiryService.GetInquiries();
            return Ok(inquiries);
        }

        // Get inquiries for a specific user
        [HttpGet("user/{userId}")]
        public IActionResult GetInquiriesToUser(int userId)
        {
            var inquiries = _inquiryService.GetInquitiesToUser(userId);
            return Ok(inquiries);
        }

        // Get inquiries for a specific property
        [HttpGet("property/{propertyId}")]
        public IActionResult GetInquiriesToProperty(int propertyId)
        {
            var inquiries = _inquiryService.GetInquitiesToPropety(propertyId);
            return Ok(inquiries);
        }

        // Create a new inquiry
        [HttpPost]
        public IActionResult CreateInquiry([FromBody] Inquiry_Create inquiry)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _inquiryService.CreateInquiry(inquiry);
            return Ok("Inquiry created successfully.");
        }

        // Update an existing inquiry
        [HttpPut("{id}")]
        public IActionResult UpdateInquiry(int id, [FromBody] Inquiry_Update inquiry)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _inquiryService.UpdateInquiry(id, inquiry);
                return Ok("Inquiry updated successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Delete an inquiry by ID
        [HttpDelete("{id}")]
        public IActionResult DeleteInquiry(int id)
        {
            try
            {
                _inquiryService.DeleteInquiry(id);
                return Ok("Inquiry deleted successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) // Catch any other exceptions
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
