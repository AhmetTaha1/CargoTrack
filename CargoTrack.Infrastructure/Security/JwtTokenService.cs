

using System.Security.Claims;
using System.Text;
using CargoTrack.Application.Interfaces;
using CargoTrack.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace CargoTrack.Infrastructure.Security;

public class JwtTokenService : ITokenService
{
    private readonly JwtSettings _settings;

    public JwtTokenService(IOptions<JwtSettings> options)
    {
        _settings=options.Value;
    }

    public string CreateToken(User user)
    {
        var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));

        var descriptor= new SecurityTokenDescriptor
        {
            Subject=new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            }),
            Issuer=_settings.Issuer,
            Audience=_settings.Audience,
            Expires=DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes),
            SigningCredentials=new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
        };
        return new JsonWebTokenHandler().CreateToken(descriptor);
    }









}