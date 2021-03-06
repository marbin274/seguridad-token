using Cryptography = System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Seguridad.Token.Extensions;
using Seguridad.Token.JWT;
using Seguridad.Token.JWT.Interfaces;
using System.Collections.Generic;

namespace Seguridad.Token.RSA
{
    public class RSAHandler : Handler, IHandler
    {
        private readonly byte[] _privateKey;
        private readonly byte[] _publicKey;
        public RSAHandler(string privateKey, string publicKey, ClientJsonConfiguration setting, ClientJsonOptions options = null)
        {
            _privateKey = privateKey.ToByteArray();
            _publicKey = publicKey.ToByteArray();
            init(SecurityAlgorithms.RsaSha256, setting, options);
        }

        public JwtResponse CreateToken(object claims)
        {
            using Cryptography.RSA rsa = Cryptography.RSA.Create();
            rsa.ImportRSAPrivateKey(_privateKey, out _);
            SecurityKey securityKey = new RsaSecurityKey(rsa);
            SecurityKey(securityKey);
            return Create(claims);
        }

        public bool ValidateToken(string token, out Dictionary<string, object> payload)
        {
            using Cryptography.RSA rsa = Cryptography.RSA.Create();
            rsa.ImportSubjectPublicKeyInfo(_publicKey, out _);
            SecurityKey securityKey = new RsaSecurityKey(rsa);
            SecurityKey(securityKey);
            var asd = Validate(token, out payload);
            return asd;
        }
    }

}
