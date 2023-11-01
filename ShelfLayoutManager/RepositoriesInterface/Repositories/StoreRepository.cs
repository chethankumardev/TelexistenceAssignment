using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShelfLayoutManager.Entity;
using ShelfLayoutManager.Model;
using ShelfLayoutManager.RepositoriesInterface.Interfaces;

namespace ShelfLayoutManager.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ShelfLayoutDbContext _context;

        public StoreRepository(ShelfLayoutDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StoreModel>> GetStoresAsync()
        {
            var stores = await _context.Stores.ToListAsync();
            return stores.Select(store => MapStoreToModel(store));
        }

        public async Task<StoreModel> GetStoreAsync(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            return store != null ? MapStoreToModel(store) : null;
        }

        public async Task<StoreModel> CreateStoreAsync(Store store)
        {
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return MapStoreToModel(store);
        }

        public async Task<StoreModel> UpdateStoreAsync(int id, StoreModel store)
        {
            var existingStore = await _context.Stores.FindAsync(id);
            if (existingStore != null)
            {
                existingStore.Name = store.Name;
                existingStore.Location = store.Location;
                await _context.SaveChangesAsync();
                return MapStoreToModel(existingStore);
            }
            return null;
        }

        public async Task<bool> DeleteStoreAsync(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                _context.Stores.Remove(store);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private StoreModel MapStoreToModel(Store store)
        {
            return new StoreModel
            {
                Id = store.Id,
                Name = store.Name,
                Location = store.Location
            };
        }
    }
}
