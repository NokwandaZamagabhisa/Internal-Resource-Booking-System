using Microsoft.EntityFrameworkCore;
using ResourceBooking.Core.Entities;
using ResourceBooking.Core.Gateways;

namespace BookingSystem.DataAccess.Resources
{
    public class SqlServerResourceGateway : IResourceGateway
    {
        private readonly ApplicationDbContext _context;

        public SqlServerResourceGateway(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Resource resource)
        {
            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var resource = await GetById(id);
            if (resource != null)
            {
                _context.Resources.Remove(resource);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Resources.AnyAsync(resource => resource.Id == id);
        }

        public async Task<IEnumerable<Resource>> GetAll()
        {
            return await _context.Resources.ToListAsync();
        }

        public async Task<IEnumerable<Resource>> GetAllAvailable()
        {
            return await _context.Resources.Where(resource=>resource.IsAvailable).ToListAsync();
        }

        public async Task<Resource> GetById(int id)
        {
            return await _context.Resources.FindAsync(id);
        }

        public async Task Update(Resource person)
        {
            _context.Entry(person).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
