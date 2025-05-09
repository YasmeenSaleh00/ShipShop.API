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

        public async Task<List<Testimonial>> GetNegativeTestimonials()
        {
            var testimonials = await _context.Testimonials.Include(c => c.Customer).Where(x => x.Rating < 3).ToListAsync();
            return testimonials;


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

        public async Task<List<Testimonial>> SortTestimonialsByCreation(string sortDirection)
        {
            IQueryable<Testimonial> query = _context.Testimonials;
            if (sortDirection == "desc")
            {
                query = query.OrderByDescending(x => x.CreatedOn);
            }
            if (sortDirection == "asc")
            {
                query = query.OrderBy(x => x.CreatedOn);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Testimonial>> SortTestimonialsById(string sortDirection)
        {
            IQueryable<Testimonial> query = _context.Testimonials;
            if (sortDirection == "desc")
            {
                query = query.OrderByDescending(x => x.Id);
            }
            if (sortDirection == "asc")
            {
                query = query.OrderBy(x => x.Id);
            }

            return await query.ToListAsync();
        }

        public async Task<List<Testimonial>> SortTestimonialsByRating(string sortDirection)
        {
            IQueryable<Testimonial> query = _context.Testimonials;
            if (sortDirection == "desc")
            {
                query = query.OrderByDescending(x => x.Rating);
            }
            if (sortDirection == "asc")
            {
                query = query.OrderBy(x => x.Rating);
            }

            return await query.ToListAsync();
        }
    }
}
