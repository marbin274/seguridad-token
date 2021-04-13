using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
namespace Minedu.Seguridad.Token.JWT
{
    public abstract class Handler
    {
        private string _securityAlgorithm;
        private SecurityKey _securityKey;
        private ClientJsonConfiguration _settings;
        private ClientJsonOptions _options = new ClientJsonOptions()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
        };

        protected void init(string securityAlgorithm, ClientJsonConfiguration setting, ClientJsonOptions options = null)
        {
            _securityAlgorithm = securityAlgorithm;

            if (setting.TimeToExpireToken.TotalSeconds <= 0)
            {
                setting.TimeToExpireToken = new TimeSpan(0, 1, 0);
            }
            _settings = setting;

            if (options != null)
            {
                _options = options;
            }
        }

        protected Handler SecurityKey(SecurityKey securityKey)
        {
            _securityKey = securityKey;
            return this;
        }

        protected JwtResponse Create(object claims)
        {
            var signingCredentials = new SigningCredentials(_securityKey, _securityAlgorithm)
            {
                CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = _options.CacheSignatureProviders }
            };

            var now = DateTime.Now;
            var unixTimeSeconds = new DateTimeOffset(now).ToUnixTimeSeconds();
            List<Claim> claimsArray = new List<Claim> {
                    new Claim(JwtRegisteredClaimNames.Iat, unixTimeSeconds.ToString(), ClaimValueTypes.Integer64),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
            List<Claim> customClaim = new List<Claim>();
            var props = claims.GetType().GetProperties();
            foreach (PropertyInfo property in props)
            {
                claimsArray.Add(new Claim(property.Name, property.GetValue(claims, null).ToString()));
            }

            var jwt = new JwtSecurityToken(
                audience: _settings.Audience,
                issuer: _settings.Issuer,
                claims: claimsArray,
                notBefore: now,
                expires: now.Add(_settings.TimeToExpireToken),
                signingCredentials: signingCredentials
            );
            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtResponse
            {
                Token = token,
                ExpiresAt = unixTimeSeconds,
            };
        }

        protected bool Validate(string token, out Dictionary<string, object> payload)
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _settings.Issuer,
                ValidAudience = _settings.Audience,
                IssuerSigningKey = _securityKey,

                CryptoProviderFactory = new CryptoProviderFactory()
                {
                    CacheSignatureProviders = _options.CacheSignatureProviders
                }
            };
            try
            {
                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                payload = ((Dictionary<string, object>)((JwtSecurityToken)validatedToken).Payload);
                
                return true;
            }
            catch
            {
                payload = null;
                return false;
            }
        }
    }
}
