using App.Domain.Identity;
using App.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers.Identity;

[ApiController]
[Route("api/[controller]/[action]")]
public class AccountController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    private readonly ILogger<AccountController> _logger;
    private readonly IConfiguration _configuration;
    
    public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILogger<AccountController> logger, IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var appUser = await _userManager.FindByEmailAsync(dto.Email);

        if (appUser == null)
        {
            _logger.LogWarning("WebApi login failed. User {User} not found!", dto.Email);
            return BadRequest("User/password error");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(appUser, dto.Password, false);
        if (result.Succeeded)
        {
            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
            var jwt = WebApp.Helpers.IdentityExtensions.GenerateJwt(
                claimsPrincipal.Claims,
                _configuration["JWT:Key"],
                _configuration["JWT:Issuer"],
                _configuration["JWT:Issuer"],
                DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
            );
            _logger.LogInformation("WebApi login success. User {User}", dto.Email);
            return Ok(new JwtResponse()
            {
                Token = jwt
            });
        }
        _logger.LogWarning("WebApi login failed. User {User} Bad password!", dto.Email);
        return BadRequest("User/Password error");
    }
    
    [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            if (appUser != null)
            {
                _logger.LogWarning("User {User} already registered", dto.Email);
                return BadRequest("User already registered!");
            }

            appUser = new App.Domain.Identity.AppUser()
            {
                Email = dto.Email,
                UserName = dto.Email
            };
            var result = await _userManager.CreateAsync(appUser, dto.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("WebApi new registration success. User {User}", dto.Email);
                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user != null)
                {
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                    var jwt = WebApp.Helpers.IdentityExtensions.GenerateJwt(
                        claimsPrincipal.Claims,
                        _configuration["JWT:Key"],
                        _configuration["JWT:Issuer"],
                        _configuration["JWT:Issuer"],
                        DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                    );
                    
                    _logger.LogInformation("WebApi login success. User {User}", dto.Email);
                    return Ok(new JwtResponse()
                    {
                        Token = jwt
                    });
                }
                else
                {
                    _logger.LogInformation("User {User} not found after creation", dto.Email);
                    return BadRequest("User not found after creation!");
                }
            }

            //var errors = result.Errors.Select(error => error.Description).ToList();
            return BadRequest();
        }
}