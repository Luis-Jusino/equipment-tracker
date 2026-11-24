using EquipmentTracker.Api.Models;

namespace EquipmentTracker.Api.Data;

public interface IEquipmentRepository
{
    Task<List<Equipment>> GetAllAsync();
    Task<Equipment?> GetByIdAsync(int id);
    Task<Equipment> AddAsync(Equipment equipment);
    Task<bool> UpdateAsync(Equipment equipment);
    Task<bool> DeleteAsync(int id);
}