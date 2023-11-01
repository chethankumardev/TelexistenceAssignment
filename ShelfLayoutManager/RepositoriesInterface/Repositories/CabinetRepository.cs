using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShelfLayoutManager.Entity;
using ShelfLayoutManager.Model;
using ShelfLayoutManager.RepositoriesInterface.Interfaces;

public class CabinetRepository : ICabinetRepository
{
    private readonly ShelfLayoutDbContext _context;

    public CabinetRepository(ShelfLayoutDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CabinetModel>> GetCabinetsAsync()
    {
        var cabinets = await _context.Cabinets
            .Select(c => new CabinetModel
            {
                // Map Cabinet entity properties to CabinetModel properties
                Number = c.Number,
                PositionX = c.PositionX,
                PositionY = c.PositionY,
                PositionZ = c.PositionZ,
                Width = c.Width,
                Depth = c.Depth,
                Height = c.Height,
            })
            .ToListAsync();

        return cabinets;
    }

    public async Task<CabinetModel> GetCabinetAsync(int id)
    {
        var cabinet = await _context.Cabinets
            .Where(c => c.Number == id)
            .Select(c => new CabinetModel
            {
                // Map Cabinet entity properties to CabinetModel properties
                Number = c.Number,
                PositionX = c.PositionX,
                PositionY = c.PositionY,
                PositionZ = c.PositionZ,
                Width = c.Width,
                Depth = c.Depth,
                Height = c.Height,
            })
            .FirstOrDefaultAsync();

        return cabinet;
    }
  
    public async Task<CabinetModel> CreateCabinetAsync(Cabinet cabinet)
    {
        var cabinetEntity = new Cabinet
        {
            Number = cabinet.Number,
            PositionX = cabinet.PositionX,
            PositionY = cabinet.PositionY,
            PositionZ = cabinet.PositionZ,
            Width = cabinet.Width,
            Depth = cabinet.Depth,
            Height = cabinet.Height,
        };

        _context.Cabinets.Add(cabinetEntity);
        await _context.SaveChangesAsync();

        return MapCabinetToModel(cabinetEntity);
    }

    public async Task<CabinetModel> UpdateCabinetAsync(int id, CabinetModel cabinet)
    {
        var existingCabinet = await _context.Cabinets
            .Where(c => c.Number == id)
            .FirstOrDefaultAsync();

        if (existingCabinet != null)
        {
            // Update the existing Cabinet entity with the new values from CabinetModel
            existingCabinet.PositionX = cabinet.PositionX;
            existingCabinet.PositionY = cabinet.PositionY;
            existingCabinet.PositionZ = cabinet.PositionZ;
            existingCabinet.Width = cabinet.Width;
            existingCabinet.Depth = cabinet.Depth;
            existingCabinet.Height = cabinet.Height;

            await _context.SaveChangesAsync();

        }


        return existingCabinet != null ?  MapCabinetToModel(existingCabinet) : null;
    }

    public async Task<bool> DeleteCabinetAsync(int id)
    {
        var cabinet = await _context.Cabinets
            .Where(c => c.Number == id)
            .FirstOrDefaultAsync();

        if (cabinet != null)
        {
            _context.Cabinets.Remove(cabinet);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }

    public CabinetModel MapCabinetToModel(Cabinet cabinet)
    {
        if (cabinet == null)
        {
            return null;
        }

        return new CabinetModel
        {
            Number = cabinet.Number,
            PositionX = cabinet.PositionX,
            PositionY = cabinet.PositionY,
            PositionZ = cabinet.PositionZ,
            Width = cabinet.Width,
            Depth = cabinet.Depth,
            Height = cabinet.Height
        };
    }

   
}
