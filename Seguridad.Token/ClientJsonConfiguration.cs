using System;

namespace Seguridad.Token
{
    public class ClientJsonConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public TimeSpan TimeToExpireToken { get; set; }
    }
}

