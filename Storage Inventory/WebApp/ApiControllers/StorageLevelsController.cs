using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class StorageLevelsController : ControllerBase
{
    private readonly AppDbContext _context;

    public StorageLevelsController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StorageLevel>>> GetStorageLevels()
    {
        return await _context.StorageLevels.ToListAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<StorageLevel>> GetStorageLevel(Guid id)
    {
        var storageLevel = await _context.StorageLevels.FindAsync(id);

        if (storageLevel == null)
        {
            return NotFound();
        }

        return storageLevel;
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutStorageLevel(Guid id, StorageLevel storageLevel)
    {
        if (id != storageLevel.Id)
        {
            return BadRequest();
        }

        _context.Entry(storageLevel).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StorageLevelExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<StorageLevel>> PostAssortmentLevel(StorageLevel storageLevel)
    {
        _context.StorageLevels.Add(storageLevel);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetStorageLevel", new { id = storageLevel.Id }, storageLevel);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteStorageLevel(Guid id)
    {
        var storageLevel = await _context.StorageLevels.FindAsync(id);
        if (storageLevel == null)
        {
            return NotFound();
        }

        _context.StorageLevels.Remove(storageLevel);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool StorageLevelExists(Guid id)
    {
        return _context.StorageLevels.Any(e => e.Id == id);
    }
}