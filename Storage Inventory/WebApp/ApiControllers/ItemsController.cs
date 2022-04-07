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
public class ItemsController : ControllerBase
{
    private readonly IAppUnitOfWork _uow;
    private readonly ItemMapper _mapper;

    public ItemsController(IMapper mapper, IAppUnitOfWork uow)
    {
        _uow = uow;
        _mapper = new ItemMapper(mapper);
    }
    
    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetItems()
    {
        var items = await _uow.Items.GetAllAsync(User.GetUserId()!.Value);
        return items.Select(item => _mapper.Map(item)).ToList()!;
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ItemDto>> GetItem(Guid id)
    {
        var item = await _uow.Items.FirstOrDefaultAsync(id, User.GetUserId()!.Value);

        if (item == null)
        {
            return NotFound();
        }

        return _mapper.Map(item)!;
    }

    [HttpGet("{keyword}")]
    public async Task<IEnumerable<ItemDto?>> FindItems(string keyword)
    {
        var items = await _uow.Items.GetAllAsync(User.GetUserId()!.Value);
        return items.Where(i => i.Title.ToLower().Contains(keyword.ToLower()) 
                                 || i.SerialNumber!= null && i.SerialNumber.ToLower().Contains(keyword.ToLower())
                                 || i.Color.ToLower().Contains(keyword.ToLower())
                                 || i.Quantity.ToString().Contains(keyword)
                                 || i.Comment.ToLower().Contains(keyword.ToLower()))
            .Select(item => _mapper.Map(item)).ToList();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutItem(Guid id, ItemDto item)
    {
        if (id != item.Id)
        {
            return BadRequest();
        }

        _uow.Items.Update(_mapper.Map(item)!);

        try
        {
            await _uow.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ItemExists(id))
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
    public async Task<ActionResult<ItemDto>> PostItem(ItemDto item)
    {
        
        _uow.Items.Add(_mapper.Map(item)!);
        await _uow.SaveChangesAsync();

        return CreatedAtAction("GetItem", new { id = item.Id }, item);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteItem(Guid id)
    {
        await _uow.Items.RemoveAsync(id, User.GetUserId()!.Value);
        await _uow.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> ItemExists(Guid id)
    {
        return await _uow.Items.ExistsAsync(id, User.GetUserId()!.Value);
    }
}
