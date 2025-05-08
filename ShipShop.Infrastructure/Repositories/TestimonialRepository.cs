using Microsoft.EntityFrameworkCore;
using ShipShop.Core.Entities;
using ShipShop.Core.Interfaces;
using ShipShop.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipShop.Infrastructure.Repositories
{
    public class TestimonialRepository : ITestimonialRepository
    {
        private readonly ShipShopDbContext _context;

        public TestimonialRepository(ShipShopDbContext context)
        {
            _context = context;
        }

        public  async Task CreateTestimonial(Testimonial testimonial)
        {
          await _context.AddAsync(testimonial);
            await _context.SaveChangesAsync();
        
        }

        public async Task<Testimonial> GetTestimonialByCustomerId(int customerId)
        {
           var testimonial = await _context.Testimonials.Include(c=>c.Customer).FirstOrDefaultAsync(z=>z.CustomerId==customerId);
            return testimonial;
        }

        public async Task<List<Testimonial>> GetTestimonials()
        {
            var testimonial = await _context.Testimonials.ToListAsync();
            return testimonial;
        }
    }
}
