using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace elevator.Controllers;

public class AuthController: ControllerBase
{
    [HttpPost]
    [Route("login")]
    public IActionResult Login( string username, string password)
    {
        if (username == "test" && password == "password")
        {
            var secretKey = new SymmetricSecurityKey("Mt5nhbt94y1/Y45S7Nfo//AtrenJ6MzsslYD7ZDzTUoeYuuU9sMaMiQ3FwHvuPBD"u8.ToArray());
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(
                issuer: "TheChucklingAtom",
                audience: "https://localhost:8080",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return Ok(new { Token = tokenString });
        }

        return Unauthorized();

    }
    
}
