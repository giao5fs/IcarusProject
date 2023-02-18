using Icarus.Application.Abstractions.Authentication;
using Icarus.Application.Members.Commands.Login;
using Icarus.Domain.Entity;
using Icarus.Infrastructure.Authentication.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Icarus.Infrastructure.Authentication.Providers;

public sealed class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;
    private readonly IClaimsProvider _claimsProvider;

    public JwtProvider(IOptions<JwtOptions> jwtOptions, IClaimsProvider claimsProvider)
    {
        _jwtOptions = jwtOptions.Value;
        _claimsProvider = claimsProvider;
    }

    public async Task<TokenResponse> GenerateTokenAsync(Member member)
    {
        var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

        var signingCredentials = new SigningCredentials(security, SecurityAlgorithms.HmacSha256);

        Claim[] claims = await _claimsProvider.GetClaimsAsync(member);

        DateTime tokenExpirationTime = DateTime.UtcNow.AddMinutes(_jwtOptions.TokenExpirationInMinutes);

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            tokenExpirationTime,
            signingCredentials);

        string valueToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new TokenResponse(valueToken);
    }
}
