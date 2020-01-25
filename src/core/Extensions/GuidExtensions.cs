using System;
using System.Security.Cryptography;
using System.Text;

namespace CSGOStats.Infrastructure.Core.Extensions
{
    public static class GuidExtensions
    {
        public static Guid Guid(this string x)
        {
            using var md5 = MD5.Create();
            return new Guid(md5.ComputeHash(Encoding.ASCII.GetBytes(x)));
        }
    }
}