using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Minedu.Seguridad.Token.JWT
{
    public abstract class Handler
    {
        private string _securityAlgorithm;
        private SecurityKey _securityKey;
        private ClientJsonConfiguration _settings;
        private TimeSpan TimeToExpireToken = new TimeSpan(0, 1, 0);
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
            _settings = setting;
            
            if (setting.TimeToExpireToken.TotalSeconds > 0)
            {
                TimeToExpireToken = setting.TimeToExpireToken;
            }

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

        protected JwtResponse Create(JwtCustomClaims claims)
        {
            var signingCredentials = new SigningCredentials(_securityKey, _securityAlgorithm)
            {
                CryptoProviderFactory = new CryptoProviderFactory { CacheSignatureProviders = _options.CacheSignatureProviders }
            };

            var now = DateTime.Now;
            var unixTimeSeconds = new DateTimeOffset(now).ToUnixTimeSeconds();

            var jwt = new JwtSecurityToken(
                audience: _settings.Audience,
                issuer: _settings.Issuer,
                claims: new Claim[] {
                    new Claim(JwtRegisteredClaimNames.Iat, unixTimeSeconds.ToString(), ClaimValueTypes.Integer64),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(nameof(claims.FirstName), claims.FirstName),
                    new Claim(nameof(claims.LastName), claims.LastName),
                    new Claim(nameof(claims.Email), claims.Email),
                    new Claim(nameof(claims.agw), claims.agw)

                },
                notBefore: now,
                expires: now.Add(TimeToExpireToken),
                signingCredentials: signingCredentials
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtResponse
            {
                Token = token,
                ExpiresAt = unixTimeSeconds,
            };
        }

        protected bool Validate(string token)
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
            SecurityToken validatedToken = null;
            try
            {
                var handler = new JwtSecurityTokenHandler();
                handler.ValidateToken(token, validationParameters, out validatedToken);
            }
            catch
            {
                return false;
            }

            return true;
        }

    }
}