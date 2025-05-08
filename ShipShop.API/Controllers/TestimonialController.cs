using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipShop.Application.Commands;
using ShipShop.Application.Services;
using ShipShop.Core.Entities;

namespace ShipShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialController : ControllerBase
    {
        private readonly TestimonialService _testimonialService;

        public TestimonialController(TestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTestimonial()
        {
            var test= await _testimonialService.GetTestimonials();
            if(test == null || test.Count==0)
            {
                return NotFound();

            }
            return Ok(test);
        }

        [HttpGet("{custId}")]
        public async Task<IActionResult> GetTestimonial(int custId)
        {
            var test = await _testimonialService.GetTestimonialByCustomerId(custId);
            if (test == null )
            {
                return NotFound();

            }
            return Ok(test);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(TestimonialCommand testimonial)
        {
            if (testimonial == null)
            {
                return BadRequest();
            }
            await _testimonialService.CreateTestimonial(testimonial);
            return Ok();
        }
    }
}
