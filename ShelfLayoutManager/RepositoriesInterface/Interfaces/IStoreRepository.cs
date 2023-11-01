using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShelfLayoutManager.Entity;
using ShelfLayoutManager.Model;

namespace ShelfLayoutManager.RepositoriesInterface.Interfaces
{
    public interface IStoreRepository
    {
        Task<IEnumerable<StoreModel>> GetStoresAsync();
        Task<StoreModel> GetStoreAsync(int id);
        Task<StoreModel> CreateStoreAsync(Store store);
        Task<StoreModel> UpdateStoreAsync(int id, StoreModel store);
        Task<bool> DeleteStoreAsync(int id);
    }
}


