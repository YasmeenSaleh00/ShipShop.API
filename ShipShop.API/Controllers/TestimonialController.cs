using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShipShop.Application.Commands;
using ShipShop.Application.Services;
using ShipShop.Core.Entities;
using static System.Net.Mime.MediaTypeNames;

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
        [HttpGet]
        [Route("sort-by-id/{sortDirection}")]
        public async Task<IActionResult> SortById(string sortDirection)
        {
            var testimonials = await _testimonialService.SortTestimonialsById(sortDirection);
            return Ok(testimonials);

        }
        [HttpGet]
        [Route("sort-by-creation-date/{sortDirection}")]
        public async Task<IActionResult> SortByCreationDate(string sortDirection)
        {
            var testimonials = await _testimonialService.SortTestimonialsByCreation(sortDirection);
            return Ok(testimonials);

        }
        [HttpGet]
        [Route("sort-by-rating/{sortDirection}")]
        public async Task<IActionResult> SortByRating(string sortDirection)
        {
            var testimonials = await _testimonialService.SortTestimonialsByRating(sortDirection);
            return Ok(testimonials);

        }
        [HttpGet]
        [Route("negative")]
        public async Task<IActionResult> GetNegativeTestimonials()
        {
          
            var testimonials = await _testimonialService.GetNegativeTestimonials();
            if (testimonials == null || testimonials.Count == 0)
            {
                return NotFound();

            }
            return Ok(testimonials);

        }
        [HttpGet]
        [Route("negative-count")]
        public async Task<IActionResult> GetNegativeTestimonialsCount()
        {
            var testimonials = await _testimonialService.GetNegativeTestimonials();
          
            return Ok(testimonials.Count);

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
