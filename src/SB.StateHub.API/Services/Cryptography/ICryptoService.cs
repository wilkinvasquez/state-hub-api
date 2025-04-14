namespace SB.StateHub.API.Services.Cryptography
{
    public interface ICryptoService
    {
        string GenerateAuthenticationToken(int? userId);
        int? ValidateAuthenticationToken(string token);
        string? GetEncodedToken();
        string GenerateSHA256(string? value);
    }
}