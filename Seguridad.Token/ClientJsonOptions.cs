
namespace Seguridad.Token
{
    public class ClientJsonOptions
    {
        public bool CacheSignatureProviders { get; set; }
        public bool ValidateIssuer { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateLifetime { get; set; }
        public bool ValidateIssuerSigningKey { get; set; }
    }
}

