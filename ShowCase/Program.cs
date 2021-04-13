using System;
using Seguridad.Token;
using Seguridad.Token.JWT.Interfaces;
using Seguridad.Token.JWT;
using Seguridad.Token.RSA;
using ShowCase.Models;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        string RsaPrivateKey = "MIICXAIBAAKBgQCPu0YVVliBAOCK2TUrkByzfG2FZUl/GpzfQ1OAaCMrO6ack5HOnnIl/mK9Sy7ZveMhgGyZE4Yg2F0w9WaxUR61xRW8VUFrwMqtPow7tUCRNOijejIIn8pA6Svu4ULjQg0NT3SOG8PqKkBKzfURcFbasm5ikL2m8Jw4MgUz6ubovwIDAQABAoGAbmTIT1siXvLtg7eQvwTRBoprFCnXaWhnVJPBbr6NRIdsUJaHRssroULhU8KLGXJfITwiLPBx9Ig6C4Bwf4ButMabnYeUI0sYbQjxfrjkhlZaDFnnm8Au0p/gMDfXeu6bLdBrg93/H7X0LqRVMEm/vVqTh6JoWIKXezbeOa7RowECQQDLl9wmgPqqwHebcvjbSgU115KUNnCzu8PjFc3wN+LHqU93Ep1Hhkari41+Jlb18oTguKTiALMEluLD6KmM77ZNAkEAtLq4yCG1cYZszRg+5IZrJcxfkO18tC5Kf2ZH4ORV45L0y2WUqo///YwHj+j9C/fo+IU7Ng/RCeKEZA34zjP5OwJBAI058V/QNpG94vo0/cV4Cjc4K5ieTv2OcSqUFH/e9HQV0WbCxdE4pssWifcI92eybFRKIS4Y2BWF6RWGzh5Spj0CQDQKPCDsi+mHkpav75QwHHRC2BXMPIdJeQwcMIBSX2TrMO/MbTV7x3ODub23kf27QeslufoELeYRwpto5obBsncCQGOQ2vlOUel7T4zf5jIUzeJ1sZou6ZEyfewXnyB1uYo71xqOxCYppbvpce6wbsLfrVlvUpJmhayq6fXCrtASEnQ=";
        string RsaPublicKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCPu0YVVliBAOCK2TUrkByzfG2FZUl/GpzfQ1OAaCMrO6ack5HOnnIl/mK9Sy7ZveMhgGyZE4Yg2F0w9WaxUR61xRW8VUFrwMqtPow7tUCRNOijejIIn8pA6Svu4ULjQg0NT3SOG8PqKkBKzfURcFbasm5ikL2m8Jw4MgUz6ubovwIDAQAB";
        ClientJsonConfiguration config = new ClientJsonConfiguration()
        {
            Audience = "You",
            Issuer = "Me",
        };
        IHandler handler = new RSAHandler(RsaPrivateKey, RsaPublicKey, config);
        var claims = new JwtCustomClaims()
        {
            agw = "agw",
            Email = "prueba.56@minedu.gob.pe",
            FirstName = "Sergio",
            LastName = "Cardenas"
        };
        JwtResponse response = handler.CreateToken(claims);
        Console.WriteLine($"Token: {response}");
        bool isTokenValid = handler.ValidateToken(response.Token, out Dictionary<string, object> payload);
        Console.WriteLine($"isValid: {isTokenValid}");
    }
}