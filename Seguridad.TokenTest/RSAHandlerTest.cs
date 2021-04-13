using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Seguridad.Token.JWT;
using Seguridad.Token.JWT.Interfaces;
using Seguridad.Token.RSA;

namespace StringLibraryTest
{
    [TestClass]
    public class RSAHandlerTest
    {
        [TestMethod]
        public void TestGenerateTokenShouldBeSuccess()
        {
            IHandler handler = new RSAHandler(Fixtures.RsaPrivateKey, Fixtures.RsaPublicKey, Fixtures.GetDefaultConfig());
            var claims = new DefaultClaims();
            JwtResponse response = handler.CreateToken(claims);
            Assert.IsTrue(response.Token.Length > 0);
            Assert.IsTrue(response.ExpiresAt > 0);
        }

        [TestMethod]
        public void TestValidateTokenShouldBeSuccess()
        {
            IHandler handler = new RSAHandler(Fixtures.RsaPrivateKey, Fixtures.RsaPublicKey, Fixtures.GetDefaultConfig());
            var claims = new DefaultClaims();
            JwtResponse response = handler.CreateToken(claims);

            bool isTokenValid = handler.ValidateToken(response.Token, out Dictionary<string, object> payload);
            Assert.IsTrue(isTokenValid);
        }

        [TestMethod]
        public void TestValidateTokenShouldNotBeSuccess()
        {
            IHandler handler = new RSAHandler(Fixtures.RsaPrivateKey, Fixtures.RsaPublicKey, Fixtures.GetDefaultConfig());
            var claims = new DefaultClaims();
            JwtResponse response = handler.CreateToken(claims);

            bool isTokenValid = handler.ValidateToken(response.Token, out Dictionary<string, object> payload);
            Assert.IsTrue(isTokenValid);
        }

        [TestMethod]
        public void TestValidateTokenShouldDataCorrect()
        {
            IHandler handler = new RSAHandler(Fixtures.RsaPrivateKey, Fixtures.RsaPublicKey, Fixtures.GetDefaultConfig());
            var claims = new DefaultClaims();
            JwtResponse response = handler.CreateToken(claims);

            handler.ValidateToken(response.Token, out Dictionary<string, object> payload);
            Assert.AreEqual(payload.First(it => it.Key == "agw").Value, claims.agw);
            Assert.AreEqual(payload.First(it => it.Key == "Email").Value, claims.Email);
            Assert.AreEqual(payload.First(it => it.Key == "FirstName").Value, claims.FirstName);
            Assert.AreEqual(payload.First(it => it.Key == "LastName").Value, claims.LastName);
        }

        [TestMethod]
        public void TestValidateTokenShouldDefaultTimeToExpireToken()
        {
            IHandler handler = new RSAHandler(Fixtures.RsaPrivateKey, Fixtures.RsaPublicKey, Fixtures.GetDefaultConfig());
            var claims = new DefaultClaims();
            JwtResponse response = handler.CreateToken(claims);

            handler.ValidateToken(response.Token, out Dictionary<string, object> payload);

            long notBefore = (long)payload.First(it => it.Key == "nbf").Value;
            long exp = (long)payload.First(it => it.Key == "exp").Value;

            TimeSpan defaultTimeToExpireToken = new TimeSpan(0, 1, 0);
             
            Assert.AreEqual(notBefore, response.ExpiresAt, "El tiempo de expiración del request creado no es igual al tiempo dentro del token");
            Assert.AreEqual(notBefore + defaultTimeToExpireToken.TotalSeconds, exp);
        }

        [TestMethod]
        public void TestValidateTokenShouldSetTimeToExpireToken()
        {
            var config = Fixtures.GetDefaultConfig();
            config.TimeToExpireToken = new TimeSpan(0, 15, 0);
            IHandler handler = new RSAHandler(Fixtures.RsaPrivateKey, Fixtures.RsaPublicKey, config);
            var claims = new DefaultClaims();
            JwtResponse response = handler.CreateToken(claims);

            handler.ValidateToken(response.Token, out Dictionary<string, object> payload);

            long notBefore = (long)payload.First(it => it.Key == "nbf").Value;
            long exp = (long)payload.First(it => it.Key == "exp").Value;
             
            Assert.AreEqual(notBefore, response.ExpiresAt, "El tiempo de expiración del request creado no es igual al tiempo dentro del token");
            Assert.AreEqual(notBefore +  config.TimeToExpireToken.TotalSeconds, exp);
        }
    }
}
