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
public class StorageLevelsController : ControllerBase
{
    private readonly IAppUnitOfWork _uow;
    private readonly StorageLevelMapper _mapper;

    public StorageLevelsController(IMapper mapper, IAppUnitOfWork uow)
    {
        _mapper = new StorageLevelMapper(mapper);
        _uow = uow;
    }
    
    [HttpGet]
    public async Task<IEnumerable<StorageLevelDto>> GetStorageLevels()
    {
        var storageLevels = await _uow.StorageLevels.GetAllAsync(User.GetUserId()!.Value);
        return storageLevels.Select(storageLevel => _mapper.Map(storageLevel)).ToList()!;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<StorageLevelDto>> GetStorageLevel(Guid id)
    {
        var storageLevel = await _uow.StorageLevels.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

        if (storageLevel == null)
        {
            return NotFound();
        }

        return _mapper.Map(storageLevel)!;
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutStorageLevel(Guid id, StorageLevelDto storageLevel)
    {
        if (id != storageLevel.Id)
        {
            return BadRequest();
        }

        _uow.StorageLevels.Update(_mapper.Map(storageLevel)!);

        try
        {
            await _uow.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await StorageLevelExists(id))
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
    public async Task<ActionResult<StorageLevelDto>> PostAssortmentLevel(StorageLevelDto storageLevel)
    {
        _uow.StorageLevels.Add(_mapper.Map(storageLevel)!);
        await _uow.SaveChangesAsync();

        return CreatedAtAction("GetStorageLevel", new { id = storageLevel.Id }, storageLevel);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteStorageLevel(Guid id)
    {
        await _uow.StorageLevels.RemoveAsync(id, User.GetUserId()!.Value);
        await _uow.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> StorageLevelExists(Guid id)
    {
        return await _uow.StorageLevels.ExistsAsync(id, User.GetUserId()!.Value);
    }
}