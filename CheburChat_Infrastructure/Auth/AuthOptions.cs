using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Auth;

public static class AuthOptions
{
    const string SECRET = "hello_hello_hello_hello_secret_key";
    public static SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new(Encoding.UTF8.GetBytes(SECRET));
    }

    public static string CreateToken(List<Claim> claims)
    {
        return new JwtSecurityTokenHandler().WriteToken(
            new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(100)),
                signingCredentials: new SigningCredentials(
                    GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256)
                )
            );
    }
}
