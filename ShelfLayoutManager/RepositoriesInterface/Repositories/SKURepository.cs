using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShelfLayoutManager.Entity;
using ShelfLayoutManager.RepositoriesInterface.Interfaces;

namespace ShelfLayoutManager.Repositories
{
    public class SKURepository : ISKURepository
    {
        private readonly ShelfLayoutDbContext _context;

        public SKURepository(ShelfLayoutDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SKU>> GetAllSKUsAsync()
        {
            return await _context.SKUs.ToListAsync();
        }

        public async Task<SKU> GetSKUByIdAsync(int id)
        {
            return await _context.SKUs.FindAsync(id);
        }

        public async Task<SKU> CreateSKUAsync(SKU sku)
        {
            _context.SKUs.Add(sku);
            await _context.SaveChangesAsync();
            return sku;
        }

        public async Task<SKU> UpdateSKUAsync(int id, SKU sku)
        {
            if (id != sku.Id)
                return null;

            _context.Entry(sku).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SKUExists(id))
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }

            return sku;
        }

        public async Task<bool> DeleteSKUAsync(int id)
        {
            var sku = await _context.SKUs.FindAsync(id);

            if (sku == null)
                return false;

            _context.SKUs.Remove(sku);
            await _context.SaveChangesAsync();
            return true;
        }

        private bool SKUExists(int id)
        {
            return _context.SKUs.Any(e => e.Id == id);
        }
    }
}
