using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SB.StateHub.API.Shared.Jwt;

namespace SB.StateHub.API.Services.Cryptography
{
    public class CryptoService : ICryptoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtSetting _jwtSetting;

        public CryptoService(IHttpContextAccessor httpContextAccessor, IOptions<JwtSetting> jwtSetting)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtSetting = jwtSetting.Value;
        }

        public string GenerateAuthenticationToken(int? userId)
        {
            string? tokenKey = _jwtSetting.Key;
            float? tokenExpiration = _jwtSetting.ExpirationInDays;
            string token = GenerateToken(userId, tokenKey, tokenExpiration);

            return token;
        }

        public int? ValidateAuthenticationToken(string token)
        {
            string? tokenKey = _jwtSetting.Key;
            int? userId = ValidateToken(token, tokenKey);

            return userId;
        }

        public string? GetEncodedToken()
        {
            try
            {
                string? encodedToken = _httpContextAccessor.HttpContext?.Request.Headers.Authorization;

                encodedToken = encodedToken!.Replace("Bearer ", "").Replace("bearer ", "");

                return encodedToken;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string GenerateSHA256(string? value)
        {
            string hash = string.Empty;
            byte[] hashValue = SHA256.HashData(Encoding.UTF8.GetBytes(value!));

            return hashValue.Aggregate(hash, (current, b) => current + $"{b:X2}");
        }

        private string GenerateToken(int? userId, string? tokenKey, float? days)
        {
            byte[] key = Encoding.ASCII.GetBytes(tokenKey!);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim("userId", userId.ToString()!)
                ]),
                Expires = DateTime.UtcNow.AddDays(days ?? 1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }


        private int? ValidateToken(string? token, string? tokenKey)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(tokenKey!);

            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
            int userId = int.Parse(jwtToken.Claims.First(x => x.Type == "userId").Value);

            return userId;
        }
    }
}