using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SEBO.API.Domain.Entities.IdentityAggregate;
using SEBO.API.Domain.Interface.Services.Identity;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace SEBO.API.Services.Identity
{
    public class TokenService : ITokenService
    {
        private readonly ApplicationJwtOptions _jwtOptions;

        public TokenService(IOptions<ApplicationJwtOptions> JwtOptions)
        {
            _jwtOptions = JwtOptions.Value;
        }
        public async Task<Result<ApplicationToken>> GetToken(IdentityUser<int> user, IList<Claim> claims, IList<string> roles)
        {
            //TODO: ARRUMAR TRY-CATCH
            try
            {
                var userClaimsRights = getUserClaims(user, claims, roles);
                var tokens = GenerateSecurityToken(userClaimsRights.accessClaims, userClaimsRights.refreshClaims);

                var generatedAccessToken = new JwtSecurityTokenHandler().WriteToken(tokens.accessToken);
                var generatedRefreshToken = new JwtSecurityTokenHandler().WriteToken(tokens.refreshToken);

                return Result.Ok(new ApplicationToken(generatedAccessToken, generatedRefreshToken, tokens.accessExpiration, tokens.refreshExpiration));
            }
            catch (Exception ex)
            {
                return Result.Fail<ApplicationToken>($"{ex.Message}");
            }
        }

        private (IEnumerable<Claim> accessClaims, IEnumerable<Claim> refreshClaims) getUserClaims(IdentityUser<int> user, IList<Claim> userClaims, IList<string> roles)
        {
            var accessClaims = new List<Claim>();

            accessClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            accessClaims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            accessClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())); //Json Token Identifier
            var now = DateTimeOffset.UtcNow;
            accessClaims.Add(new Claim(JwtRegisteredClaimNames.Nbf, now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));
            accessClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));

            var refreshClaims = new List<Claim>(accessClaims);

            foreach (var userClaim in userClaims)
            {
                accessClaims.Add(userClaim);
            }

            foreach (var role in roles)
            {
                accessClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            return (accessClaims, refreshClaims);
        }

        private (JwtSecurityToken accessToken, JwtSecurityToken refreshToken, DateTime accessExpiration, DateTime refreshExpiration)
            GenerateSecurityToken(IEnumerable<Claim> accessClaims, IEnumerable<Claim> refreshClaims)
        {
            var accessTokenExpiration = DateTime.Now.AddSeconds(_jwtOptions.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddSeconds(_jwtOptions.RefreshTokenExpiration);

            var accessToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: accessClaims,
                notBefore: DateTime.Now,
                expires: accessTokenExpiration,
                signingCredentials: _jwtOptions.SigningCredentials
            );

            var refreshToken = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: refreshClaims,
                notBefore: DateTime.Now,
                expires: refreshTokenExpiration,
                signingCredentials: _jwtOptions.SigningCredentials
            );

            return (accessToken, refreshToken, accessTokenExpiration, refreshTokenExpiration);
        }
    }
}