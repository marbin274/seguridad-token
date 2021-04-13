
using Seguridad.Token;

namespace StringLibraryTest
{
    public static class Fixtures
    {
        public static string RsaPrivateKey = "MIICXAIBAAKBgQCPu0YVVliBAOCK2TUrkByzfG2FZUl/GpzfQ1OAaCMrO6ack5HOnnIl/mK9Sy7ZveMhgGyZE4Yg2F0w9WaxUR61xRW8VUFrwMqtPow7tUCRNOijejIIn8pA6Svu4ULjQg0NT3SOG8PqKkBKzfURcFbasm5ikL2m8Jw4MgUz6ubovwIDAQABAoGAbmTIT1siXvLtg7eQvwTRBoprFCnXaWhnVJPBbr6NRIdsUJaHRssroULhU8KLGXJfITwiLPBx9Ig6C4Bwf4ButMabnYeUI0sYbQjxfrjkhlZaDFnnm8Au0p/gMDfXeu6bLdBrg93/H7X0LqRVMEm/vVqTh6JoWIKXezbeOa7RowECQQDLl9wmgPqqwHebcvjbSgU115KUNnCzu8PjFc3wN+LHqU93Ep1Hhkari41+Jlb18oTguKTiALMEluLD6KmM77ZNAkEAtLq4yCG1cYZszRg+5IZrJcxfkO18tC5Kf2ZH4ORV45L0y2WUqo///YwHj+j9C/fo+IU7Ng/RCeKEZA34zjP5OwJBAI058V/QNpG94vo0/cV4Cjc4K5ieTv2OcSqUFH/e9HQV0WbCxdE4pssWifcI92eybFRKIS4Y2BWF6RWGzh5Spj0CQDQKPCDsi+mHkpav75QwHHRC2BXMPIdJeQwcMIBSX2TrMO/MbTV7x3ODub23kf27QeslufoELeYRwpto5obBsncCQGOQ2vlOUel7T4zf5jIUzeJ1sZou6ZEyfewXnyB1uYo71xqOxCYppbvpce6wbsLfrVlvUpJmhayq6fXCrtASEnQ=";
        public static string RsaPublicKey = "MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCPu0YVVliBAOCK2TUrkByzfG2FZUl/GpzfQ1OAaCMrO6ack5HOnnIl/mK9Sy7ZveMhgGyZE4Yg2F0w9WaxUR61xRW8VUFrwMqtPow7tUCRNOijejIIn8pA6Svu4ULjQg0NT3SOG8PqKkBKzfURcFbasm5ikL2m8Jw4MgUz6ubovwIDAQAB";

        public static string tokenDefault = "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJpYXQiOjE2MTgyMzg4NzksImp0aSI6IjMzYjY5ZjEyLTRlMWYtNDg0NS1hMWQwLTNmYjNmMmQwOGMyZiIsImFndyI6ImFndyIsIkVtYWlsIjoicHJ1ZWJhLjU2QG1pbmVkdS5nb2IucGUiLCJGaXJzdE5hbWUiOiJTZXJnaW8iLCJMYXN0TmFtZSI6IkNhcmRlbmFzIiwibmJmIjoxNjE4MjM4ODc5LCJleHAiOjE2MTgyMzg5MzksImlzcyI6Ik1lIiwiYXVkIjoiWW91In0.GA8-2fE5Q9noSL6wDK8oZbrdI8LltgCaLdjdh05Uv6uZKauzcQflJiqYv4sPeYquVQaZ2Dxge24dVtlLcKB3-PZsGgURTMJzJ3oYXDuLBgyqnp4tkvMABfzg6cP_vaOjLGPGIcDq_epRV7FEsa3g0t-bLLVbvo8ZMEhuVWUQUMo";

        public static long expiresAtDefault = 1618238879;

        public static ClientJsonConfiguration GetDefaultConfig()
        {
            return new ClientJsonConfiguration()
            {
                Audience = "You",
                Issuer = "Me",
            };
        }
    }

    public class DefaultClaims
    {
        public string agw { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public DefaultClaims()
        {
            agw = "agw";
            Email = "prueba.56@minedu.gob.pe";
            FirstName = "Sergio";
            LastName = "Cardenas";
        }
    }
}
