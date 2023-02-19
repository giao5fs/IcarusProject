using Icarus.Application.Abstractions.Authentication;
using Icarus.Application.Members.Commands.Login;
using Icarus.Domain.Entities;
using Icarus.Infrastructure.Authentication.Abstractions;
using Icarus.Infrastructure.Authentication.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Icarus.Infrastructure.Authentication.Providers;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _configuration;
    private readonly IClaimsProvider _claimsProvider;

    public JwtProvider(IClaimsProvider claimsProvider, IOptions<JwtOptions> configuration)
    {
        _claimsProvider = claimsProvider;
        _configuration = configuration.Value;
    }

    public async Task<TokenResponse> GenerateTokenAsync(Member member)
    {
        var SecretKey = _configuration.SecretKey;
        var TokenExpirationInMinutes = _configuration.TokenExpirationInMinutes;
        var Issuer = _configuration.Issuer;
        var Audience = _configuration.Audience;

        var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

        var signingCredentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);

        Claim[] claims = await _claimsProvider.GetClaimsAsync(member);

        DateTime tokenExpirationTime = DateTime.UtcNow.AddMinutes(TokenExpirationInMinutes);

        var token = new JwtSecurityToken(
            Issuer,
            Audience,
            claims,
            null,
            tokenExpirationTime,
            signingCredentials);

        string valueToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new TokenResponse(valueToken);
    }
}
