﻿using System.Security.Cryptography;
using System.Text;

namespace authentication.domain.Services
{
    public static class HashPassService
    {
        public static string GenerateSaltedHash(string pass, string saltpass)
        {
            var algorithm = new SHA256Managed();
            var plainText = Encoding.ASCII.GetBytes(pass);
            var salt = Encoding.ASCII.GetBytes(saltpass);

            var plainTextWithSaltBytes = new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            var data = algorithm.ComputeHash(plainTextWithSaltBytes);

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static bool CompareByteArrays(string pass1, string pass2)
        {
            var array1 = Encoding.UTF8.GetBytes(pass1);
            var array2 = Encoding.UTF8.GetBytes(pass2);

            if (array1.Length != array2.Length)
            {
                return false;
            }

            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
