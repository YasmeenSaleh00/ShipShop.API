using ShipShop.Application.Commands;
using ShipShop.Application.Models;
using ShipShop.Core.Entities;
using ShipShop.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Application.Services
{
    public class TestimonialService
    {
        private readonly ITestimonialRepository _testimonialRepository;

        public TestimonialService(ITestimonialRepository testimonialRepository)
        {
            _testimonialRepository = testimonialRepository;
        }
        public async Task CreateTestimonial(TestimonialCommand command)
        {
            if (command == null)
                return;
            var testimonial = new Testimonial()
            {
                Name= command.Name,
                Description= command.Description,
                Rating= command.Rating, 
                CustomerId= command.CustomerId,
            };
            await _testimonialRepository.CreateTestimonial(testimonial);
        }

        public async Task<List<TestimonialModel>> GetTestimonials()
        {
            var testimonial = await _testimonialRepository.GetTestimonials();
            List<TestimonialModel> testimonialModels = new List<TestimonialModel>();

            testimonialModels = testimonial.Select(x => new TestimonialModel
            {
                Id= x.Id,
                Name= x.Name,
                Description = x.Description,
                Rating= x.Rating,
                CustomerId= x.CustomerId,

            }).ToList();   
            return testimonialModels;
        }
        public async Task<TestimonialModel> GetTestimonialByCustomerId(int customerId)
        {
            var testimonial= await _testimonialRepository.GetTestimonialByCustomerId(customerId);
            if (testimonial == null)
                return null;
            TestimonialModel test = new TestimonialModel();
            test.Id=testimonial.Id;
            test.CustomerId = testimonial.CustomerId;
            test.Name=testimonial.Name;
            test.Description=testimonial.Description;
            test.Rating=testimonial.Rating;
            test.CreatedOn = testimonial.CreatedOn.ToShortDateString();
            return test;
        }
    }
}
