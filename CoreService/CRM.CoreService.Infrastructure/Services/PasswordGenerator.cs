using CRM.CoreService.Application.Interfaces.Infrastructure;

namespace CRM.CoreService.Infrastructure.Services
{
    public class PasswordGenerator : IPasswordGenerator
    {
        private readonly Random random = new Random();
        private const string UpperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string LowerCase = "abcdefghijklmnopqrstuvwxyz";
        private const string Digits = "0123456789";
        private const string SpecialChars = "!@#$%^&*()-_=+[{]}|;:,<>?/~\"";

        private const string AllChars = UpperCase + LowerCase + Digits + SpecialChars;

        public string GeneratePassword(int length)
        {
            if (length < 4)
                throw new ArgumentException("Password length must be at least 4 to meet the requirements.");

            char upper = UpperCase[random.Next(UpperCase.Length)];
            char lower = LowerCase[random.Next(LowerCase.Length)];
            char digit = Digits[random.Next(Digits.Length)];
            char special = SpecialChars[random.Next(SpecialChars.Length)];

            string remainingChars = new string(Enumerable.Range(0, length - 4)
                .Select(_ => AllChars[random.Next(AllChars.Length)])
                .ToArray());

            string password = upper.ToString() + lower + digit + special + remainingChars;
            password = new string(password.ToCharArray().OrderBy(_ => random.Next()).ToArray());

            return password;
        }
    }

}
