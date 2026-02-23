using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordHashGenerator
{
    class run
    {
        static void Main(string[] args)
        {
            Console.Write("Enter password: ");
            string password = Console.ReadLine() ?? string.Empty;

            CreateHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            string hashBase64 = Convert.ToBase64String(passwordHash);
            string saltBase64 = Convert.ToBase64String(passwordSalt);

            Console.WriteLine("\nGenerated Values:");
            Console.WriteLine($"PasswordHash = Convert.FromBase64String(\"{hashBase64}\");");
            Console.WriteLine($"PasswordSalt = Convert.FromBase64String(\"{saltBase64}\");");
        }

        private static void CreateHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                passwordSalt = hmac.Key;
            }
        }
    }
}
