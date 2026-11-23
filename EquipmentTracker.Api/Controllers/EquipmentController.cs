using EquipmentTracker.Api.Data;
using EquipmentTracker.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace EquipmentTracker.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentController : ControllerBase
{
    private readonly IEquipmentRepository _repo;

    public EquipmentController(IEquipmentRepository repo) => _repo = repo;

    //GET api/equipment?status=NeedsMaintenance
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Equipment>>> GetAll([FromQuery] EquipmentStatus? status)
    {
        var items = await _repo.GetAllSync();
        if (status is not null)
        {
            items = items.Where(e => e.Status == status).ToList();
        }
        return Ok(items);
    }

    //GET api/equipment/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Equipment>> GetById(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    // POST api/equipment
    [HttpPost]
    public async Task<ActionResult<Equipment>> Create(Equipment equipment)
    {
        var created = await _repo.AddAsync(equipment);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    //PUT api/equipment/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, Equipment equipment)
    {
        if (id != equipment.Id)
            return BadRequest("The ID in URL does not match ID in body.");
        return await _repo.UpdateAsync(equipment) ? NoContent() : NotFound();
    }

    //DELETE api/equipment/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        return await _repo.DeleteAsync(id) ? NoContent() : NotFound();
    }
}
