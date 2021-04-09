using System;
using Minedu.Seguridad.Token;
using Minedu.Seguridad.Token.JWT.Interfaces;
using Minedu.Seguridad.Token.JWT;

class Program
{
    static void Main(string[] args)
    {
        ExternalClientJsonConfiguration config = new ExternalClientJsonConfiguration(){
            Audience= "a",
            Issuer = "ab",
            ReferralId = "abc",
            ReferralUrl = "ddd",
            RsaPrivateKey = "MIICXAIBAAKBgQCPu0YVVliBAOCK2TUrkByzfG2FZUl/GpzfQ1OAaCMrO6ack5HOnnIl/mK9Sy7ZveMhgGyZE4Yg2F0w9WaxUR61xRW8VUFrwMqtPow7tUCRNOijejIIn8pA6Svu4ULjQg0NT3SOG8PqKkBKzfURcFbasm5ikL2m8Jw4MgUz6ubovwIDAQABAoGAbmTIT1siXvLtg7eQvwTRBoprFCnXaWhnVJPBbr6NRIdsUJaHRssroULhU8KLGXJfITwiLPBx9Ig6C4Bwf4ButMabnYeUI0sYbQjxfrjkhlZaDFnnm8Au0p/gMDfXeu6bLdBrg93/H7X0LqRVMEm/vVqTh6JoWIKXezbeOa7RowECQQDLl9wmgPqqwHebcvjbSgU115KUNnCzu8PjFc3wN+LHqU93Ep1Hhkari41+Jlb18oTguKTiALMEluLD6KmM77ZNAkEAtLq4yCG1cYZszRg+5IZrJcxfkO18tC5Kf2ZH4ORV45L0y2WUqo///YwHj+j9C/fo+IU7Ng/RCeKEZA34zjP5OwJBAI058V/QNpG94vo0/cV4Cjc4K5ieTv2OcSqUFH/e9HQV0WbCxdE4pssWifcI92eybFRKIS4Y2BWF6RWGzh5Spj0CQDQKPCDsi+mHkpav75QwHHRC2BXMPIdJeQwcMIBSX2TrMO/MbTV7x3ODub23kf27QeslufoELeYRwpto5obBsncCQGOQ2vlOUel7T4zf5jIUzeJ1sZou6ZEyfewXnyB1uYo71xqOxCYppbvpce6wbsLfrVlvUpJmhayq6fXCrtASEnQ=",
            RsaPublicKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCPu0YVVliBAOCK2TUrkByzfG2FZUl/GpzfQ1OAaCMrO6ack5HOnnIl/mK9Sy7ZveMhgGyZE4Yg2F0w9WaxUR61xRW8VUFrwMqtPow7tUCRNOijejIIn8pA6Svu4ULjQg0NT3SOG8PqKkBKzfURcFbasm5ikL2m8Jw4MgUz6ubovwIDAQAB"
        };
        IHandler handler = new Handler(config);
        var claims = new JwtCustomClaims(){
            agw = "agw",
            Email = "email",
            FirstName = "first name",
            LastName = "lasName"
        };
        var token = handler.CreateToken(claims);
        Console.WriteLine($"Token: {token}");
        bool isTokenValid = handler.ValidateToken(token.Token);
        Console.WriteLine($"isValid: {isTokenValid}");
    }
}