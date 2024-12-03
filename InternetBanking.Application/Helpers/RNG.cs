using System.Globalization;
using System.Text;

namespace InternetBanking.Application.Helpers
{
    public static class RNG
    {
        private static readonly Random rand = new();
        public static string SequenceOfLength(int n)
        {
            StringBuilder result = new(n);
            for (int i = 0; i < n; i++)
            {
                int randomNumber = rand.Next(0, 10);
                result.Append(randomNumber);
            }

            return result.ToString();
        }
    }
}