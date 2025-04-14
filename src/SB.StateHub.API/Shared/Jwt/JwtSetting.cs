namespace SB.StateHub.API.Shared.Jwt
{
    public class JwtSetting
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int? ExpirationInDays { get; set; }
    }
}