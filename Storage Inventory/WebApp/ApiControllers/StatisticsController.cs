using System.Collections;
using App.Contracts.DAL;
using App.Domain.Identity;
using App.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApp.ApiControllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "admin")]
public class StatisticsController : Controller
{
    private readonly IAppUnitOfWork _uow;
    private readonly UserManager<AppUser> _manager;

    public StatisticsController(IAppUnitOfWork unitOfWork, UserManager<AppUser> userManager)
    {
        _uow = unitOfWork;
        _manager = userManager;
    }
    
    [HttpGet]
    public async Task<UserDto> GetStatistics()
    {
        var usersData = new UserDto();
        var users = await _manager.Users.ToListAsync();
        usersData.UserCount = users.Count;
        var data = users.Select(user => new StatisticsDto { 
                UserEmail = user.Email, 
                ItemsTotal = _uow.Items.GetAllAsync(user.Id).Result.Sum(i => i.Quantity), 
                StorageLevelTotal = _uow.StorageLevels.GetAllAsync(user.Id).Result.Count(), 
                ItemsUniqueTotal = _uow.Items.GetAllAsync(user.Id).Result.Count() }
        ).ToList();
        usersData.Statistics = data;
        return usersData;
    } 
}