using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShelfLayoutManager.Entity;

namespace ShelfLayoutManager.RepositoriesInterface.Interfaces
{
    public interface ISKURepository
    {
        Task<IEnumerable<SKU>> GetAllSKUsAsync();
        Task<SKU> GetSKUByIdAsync(int id);
        Task<SKU> CreateSKUAsync(SKU sku);
        Task<SKU> UpdateSKUAsync(int id, SKU sku);
        Task<bool> DeleteSKUAsync(int id);
    }
}

