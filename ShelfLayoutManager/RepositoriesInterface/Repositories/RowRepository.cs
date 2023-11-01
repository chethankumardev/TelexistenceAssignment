using System;
using Microsoft.EntityFrameworkCore;
using ShelfLayoutManager.Entity;
using ShelfLayoutManager.RepositoriesInterface.Interfaces;

namespace ShelfLayoutManager.RepositoriesInterface.Repositories
{
	public class RowRepository : IRowRepository
    {
        private readonly ShelfLayoutDbContext _context;

        public RowRepository(ShelfLayoutDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Row>> GetAllRowsAsync()
        {
            return await _context.Rows.ToListAsync();
        }

        public async Task<Row> GetRowByIdAsync(int rowId)
        {
            return await _context.Rows.FindAsync(rowId);
        }

        public async Task CreateRowAsync(Row row)
        {
            _context.Rows.Add(row);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRowAsync(Row row)
        {
            _context.Entry(row).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteRowAsync(int rowId)
        {
            var row = await _context.Rows.FindAsync(rowId);
            if (row != null)
            {
                _context.Rows.Remove(row);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}

