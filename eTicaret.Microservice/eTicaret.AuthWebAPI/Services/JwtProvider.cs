using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using eTicaret.AuthWebAPI.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace eTicaret.AuthWebAPI.Services;

public sealed class JwtProvider(IOptions<JwtOptions> options)
{
    public string CreateToken()
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier,Guid.CreateVersion7().ToString()),
            new Claim("UserName","Taner Saydam"),
        };

        DateTime expires = DateTime.Now.AddDays(1);
        string refreshToken = Guid.CreateVersion7().ToString();
        string secretKey = options.Value.SecretKey;

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(secretKey));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512);

        JwtSecurityToken jwtSecurity = new(
            issuer: options.Value.Issuer,
            audience: options.Value.Audience,
            claims: claims,
            notBefore: DateTime.Now,
            expires: expires,
            signingCredentials: signingCredentials);

        JwtSecurityTokenHandler handler = new();
        string token = handler.WriteToken(jwtSecurity);

        return token;
    }
}
