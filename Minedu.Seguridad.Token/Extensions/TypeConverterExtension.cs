using System;

namespace Minedu.Seguridad.Token.Extensions
{
    public static class TypeConverterExtension
    {
        public static byte[] ToByteArray(this string value) =>
         Convert.FromBase64String(value);
    }
}