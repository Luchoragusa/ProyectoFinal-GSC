using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace WebApplicationAPI.Helpers
{
    public class Hash
    {
        public static string HashPassword(string password)
        {
            // Create a SHA256 hash from string   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computing Hash - returns here byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // now convert byte array to a string   
                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                return stringbuilder.ToString();
            }
        }
    }
}

