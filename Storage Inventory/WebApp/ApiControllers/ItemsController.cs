using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class ItemsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ItemsController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetItems()
    {
        return await _context.Items.ToListAsync();
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Item>> GetItem(Guid id)
    {
        var item = await _context.Items.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        return item;
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutItem(Guid id, Item item)
    {
        if (id != item.Id)
        {
            return BadRequest();
        }

        _context.Entry(item).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ItemExists(id))
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
    public async Task<ActionResult<Item>> PostItem(Item item)
    {
        _context.Items.Add(item);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetItem", new { id = item.Id }, item);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteItem(Guid id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        _context.Items.Remove(item);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ItemExists(Guid id)
    {
        return _context.Items.Any(e => e.Id == id);
    }
}
