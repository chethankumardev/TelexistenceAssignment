using Microsoft.EntityFrameworkCore;
using ShelfLayoutManager.Entity;
using ShelfLayoutManager.RepositoriesInterface.Interfaces;

namespace ShelfLayoutManager.RepositoriesInterface.Repositories
{
    public class LaneRepository : ILaneRepository
    {
        private readonly ShelfLayoutDbContext _context;

        public LaneRepository(ShelfLayoutDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lane>> GetAllLanesAsync()
        {
            return await _context.Lanes.ToListAsync();
        }

        public async Task<Lane> GetLaneByIdAsync(int number)
        {
            return await _context.Lanes.FindAsync(number);
        }

        public async Task CreateLaneAsync(Lane lane)
        {
            _context.Lanes.Add(lane);
            await _context.SaveChangesAsync();

        }

        public async Task UpdateLaneAsync(Lane lane)
        {
            _context.Entry(lane).State = EntityState.Modified;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<bool> DeleteLaneAsync(int laneId)
        {
            var lane = await _context.Lanes.FindAsync(laneId);
            if (lane != null)
            {
                _context.Lanes.Remove(lane);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

