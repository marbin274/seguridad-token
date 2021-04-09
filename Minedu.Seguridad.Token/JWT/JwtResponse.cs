namespace Minedu.Seguridad.Token.JWT
{
    public class JwtResponse
    {
        public string Token { get; set; }
        public long ExpiresAt { get; set; }
    }
}