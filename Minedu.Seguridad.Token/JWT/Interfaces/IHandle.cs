namespace Minedu.Seguridad.Token.JWT.Interfaces
{
    public interface IHandler
    {
        JwtResponse CreateToken(object claims);
        bool ValidateToken(string token);
    }
}