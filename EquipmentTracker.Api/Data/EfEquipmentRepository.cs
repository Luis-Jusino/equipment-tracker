using EquipmentTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipmentTracker.Api.Data;

public class EfEquipmentRepository : IEquipmentRepository
{
    private readonly AppDbContext _db;

    public EfEquipmentRepository(AppDbContext db) => _db = db;

    public Task<List<Equipment>> GetAllAsync() => _db.Equipment.AsNoTracking().ToListAsync();

    public async Task<Equipment?> GetByIdAsync(int id) => await _db.Equipment.FindAsync(id);

    public async Task<Equipment> AddAsync(Equipment equipment)
    {
        _db.Equipment.Add(equipment);
        await _db.SaveChangesAsync();
        return equipment;
    }

    public async Task<bool> UpdateAsync(Equipment equipment)
    {
        if (!await _db.Equipment.AnyAsync(e => e.Id == equipment.Id)) return false;
        _db.Equipment.Update(equipment);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _db.Equipment.FindAsync(id);
        if (item is null) return false;
        _db.Equipment.Remove(item);
        await _db.SaveChangesAsync();
        return true;
    }

}