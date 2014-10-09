using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNet.Identity;
using OpenDeploymentManager.Common.Diagnostics;

namespace OpenDeploymentManager.Server.Host.DataAccess
{
    public static class IdentityUserStoreSupport
    {
        public static string ToHex(this byte[] bytes)
        {
            var stringBuilder = new StringBuilder(bytes.Length * 2);
            for (int index = 0; index < bytes.Length; ++index)
            {
                stringBuilder.Append(bytes[index].ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        public static byte[] FromHex(this string hex)
        {
            hex.ArgumentNotNull("hex");
            if (hex.Length % 2 != 0)
            {
                throw new ArgumentException("Hex string must be an even number of characters to convert to bytes.");
            }

            var numArray = new byte[hex.Length / 2];
            int startIndex = 0;
            int index = 0;
            while (startIndex < hex.Length)
            {
                numArray[index] = Convert.ToByte(hex.Substring(startIndex, 2), 16);
                startIndex += 2;
                ++index;
            }

            return numArray;
        }

        public static IList<T> ToIList<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.ToList();
        }

        public static string GetLoginId(this UserLoginInfo login)
        {
            using (var cryptoServiceProvider = new SHA1CryptoServiceProvider())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(login.LoginProvider + "|" + login.ProviderKey);
                return "IdentityUserLogins/" + cryptoServiceProvider.ComputeHash(bytes).ToHex();
            }
        }
    }
}