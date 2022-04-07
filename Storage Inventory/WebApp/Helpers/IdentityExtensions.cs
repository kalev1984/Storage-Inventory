using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebApp.Helpers;

public static class IdentityExtensions
{
    public static Guid? GetUserId(this ClaimsPrincipal user)
    {
        var userId = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return userId != null ? Guid.Parse(userId) : Guid.Empty;
    }
        
    public static string GenerateJwt(IEnumerable<Claim> claims, string key, string issuer, string audience, DateTime expirationTime)
    {
        var signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var signinCredentials = new SigningCredentials(signinKey, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer, 
            audience, 
            claims, 
            expires: expirationTime, 
            signingCredentials: signinCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}