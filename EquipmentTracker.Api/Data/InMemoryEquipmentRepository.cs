using EquipmentTracker.Api.Models;

namespace EquipmentTracker.Api.Data;

public class InMemoryEquipmentRepository : IEquipmentRepository
{
    private readonly List<Equipment> _items = new();
    private readonly object _lock = new();

    private int _nextId = 1;

    public Task<List<Equipment>> GetAllSync()
    {
        lock (_lock) return Task.FromResult(_items.ToList());
    }

    public Task<Equipment?> GetByIdAsync(int id)
    {
        lock (_lock) return Task.FromResult(_items.FirstOrDefault(e => e.Id == id));
    }

    public Task<Equipment> AddAsync(Equipment equipment)
    {
        lock (_lock)
        {
            equipment.Id = _nextId++;
            _items.Add(equipment);
            return Task.FromResult(equipment);
        }
    }

    public Task <bool> UpdateAsync(Equipment equipment)
    {
        lock (_lock)
        {
            var index = _items.FindIndex(e => e.Id == equipment.Id);
            if (index == -1) return Task.FromResult(false);
            _items[index] = equipment;
            return Task.FromResult(true);
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock (_lock) return Task.FromResult(_items.RemoveAll(e => e.Id == id) > 0);
    }
}