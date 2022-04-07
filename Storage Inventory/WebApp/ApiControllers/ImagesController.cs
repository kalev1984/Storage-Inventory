using App.Contracts.DAL;
using App.DTO;
using App.DTO.Mappers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Helpers;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ImagesController : ControllerBase
{
    private readonly IAppUnitOfWork _uow;
    private readonly ImageMapper _mapper;

    public ImagesController(IAppUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = new ImageMapper(mapper);
    }
    
    [HttpGet]
    public async Task<IEnumerable<ImageDto>> GetImages()
    {
        var images = await _uow.Images.GetAllAsync(User.GetUserId()!.Value);
        return images.Select(image => _mapper.Map(image)).ToList()!;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ImageDto>> GetImage(Guid id)
    {
        var image = await _uow.Images.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

        if (image == null)
        {
            return NotFound();
        }

        return _mapper.Map(image)!;
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutImage(Guid id, ImageDto image)
    {
        if (id != image.Id)
        {
            return BadRequest();
        }

        _uow.Images.Update(_mapper.Map(image)!);

        try
        {
            await _uow.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ImageExists(id))
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
    public async Task<ActionResult<ImageDto>> PostImage(ImageDto image)
    {
        _uow.Images.Add(_mapper.Map(image)!);
        await _uow.SaveChangesAsync();

        return CreatedAtAction("GetImage", new { id = image.Id }, image);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteImage(Guid id)
    {
        var image = await _uow.Images.FirstOrDefaultAsync(id, User.GetUserId()!.Value);
        if (image == null)
        {
            return NotFound();
        }

        _uow.Images.Remove(image, User.GetUserId()!.Value);
        await _uow.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> ImageExists(Guid id)
    {
        return await _uow.Images.ExistsAsync(id, User.GetUserId()!.Value);
    }
}