namespace Minedu.Seguridad.Token.JWT.Interfaces
{
    public interface IHandler
    {
        JwtResponse CreateToken(JwtCustomClaims claims);
        bool ValidateToken(string token);
        string GenerateLink(string token);
    }
}