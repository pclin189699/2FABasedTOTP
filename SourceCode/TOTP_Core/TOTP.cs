using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TOTP_Core
{
    public class TOTP
    {
        private readonly RandomNumberGenerator? randomNumberGenerator;
        private long NowTimeStamps = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        private const string BASE32_CHARACTERS = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ234567=";  // padding char;

        public string CreateSecret(int length = 16)
        {
            if (length < 16 || 32 < length)
            {
                throw new ArgumentException();
            }

            byte[] byteIndex = new byte[length];
            randomNumberGenerator!.GetBytes(byteIndex);

            StringBuilder secretResult = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                int charIndex = byteIndex[i] & 31;
                secretResult.Append(BASE32_CHARACTERS[charIndex]);
            }
            return secretResult.ToString();
        }
    }
}
