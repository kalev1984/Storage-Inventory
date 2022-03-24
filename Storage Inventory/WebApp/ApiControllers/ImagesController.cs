#nullable disable
using App.DAL.EF;
using App.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private readonly AppDbContext _context;

    public ImagesController(AppDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Image>>> GetImages()
    {
        return await _context.Images.ToListAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Image>> GetImage(Guid id)
    {
        var image = await _context.Images.FindAsync(id);

        if (image == null)
        {
            return NotFound();
        }

        return image;
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutImage(Guid id, Image image)
    {
        if (id != image.Id)
        {
            return BadRequest();
        }

        _context.Entry(image).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ImageExists(id))
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
    public async Task<ActionResult<Image>> PostImage(Image image)
    {
        _context.Images.Add(image);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetImage", new { id = image.Id }, image);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteImage(Guid id)
    {
        var image = await _context.Images.FindAsync(id);
        if (image == null)
        {
            return NotFound();
        }

        _context.Images.Remove(image);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ImageExists(Guid id)
    {
        return _context.Images.Any(e => e.Id == id);
    }
}