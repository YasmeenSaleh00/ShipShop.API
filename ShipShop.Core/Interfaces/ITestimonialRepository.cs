using ShipShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Core.Interfaces
{
    public interface ITestimonialRepository
    {
        Task CreateTestimonial(Testimonial testimonial);    
        Task<List<Testimonial>> GetTestimonials();
        Task<Testimonial> GetTestimonialByCustomerId(int customerId);
        Task<List<Testimonial>> SortTestimonialsById(string sortDirection);
        Task<List<Testimonial>> SortTestimonialsByRating(string sortDirection);
        Task<List<Testimonial>> SortTestimonialsByCreation(string sortDirection);
        Task<List<Testimonial>> GetNegativeTestimonials();
    }
}
