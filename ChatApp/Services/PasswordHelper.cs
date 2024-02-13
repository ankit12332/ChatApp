using System.Security.Cryptography;

namespace ChatApp.Services
{
    public class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            using var hmac = new HMACSHA512();
            var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(passwordHash);
        }

        // This method can be used for verifying the password at login
        public static bool VerifyPassword(string hashedPassword, string password)
        {
            using var hmac = new HMACSHA512();
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            var originalHash = Convert.FromBase64String(hashedPassword);
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != originalHash[i]) return false;
            }
            return true;
        }
    }
}
