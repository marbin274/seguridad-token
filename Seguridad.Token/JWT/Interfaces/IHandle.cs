using System.Collections.Generic;

namespace Seguridad.Token.JWT.Interfaces
{
    public interface IHandler
    {
        JwtResponse CreateToken(object claims);
        bool ValidateToken(string token, out Dictionary<string, object> payload);
    }
}