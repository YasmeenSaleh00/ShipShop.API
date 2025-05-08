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
    }
}
