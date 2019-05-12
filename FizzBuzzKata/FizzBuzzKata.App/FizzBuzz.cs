using System;

namespace FizzBuzzKata.App
{
    public class FizzBuzz
    {
        public string GetNumber(int i)
        {
            AssertValidityParameters(i);
            
            if (HasToShowFizzFuzz(i)) return "FizzFuzz";
            if (HasToShowFizz(i)) return "Fizz";
            if (HasToShowFuzz(i)) return "Fuzz";

            return $"{i}";
        }

        private static bool HasToShowFizz(int i) => i % 3 == 0;

        private static bool HasToShowFuzz(int i) => i % 5 == 0;

        private static bool HasToShowFizzFuzz(int i) => HasToShowFizz(i) && HasToShowFuzz(i);

        private static void AssertValidityParameters(int i)
        {
            var isOutOfRange = i < 1 || i > 100;
            if (isOutOfRange) throw new ArgumentOutOfRangeException(nameof(i));
        }
    }
}
