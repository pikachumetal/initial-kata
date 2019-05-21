using System.Collections.Generic;
using System.Text;

namespace RomanToDecimal.App
{
    public class RomanNumber
    {
        private readonly IDictionary<string, int> _romanValuesMap = new Dictionary<string, int>()
        {
            { "I", 1 },
            { "V", 5 },
            { "X", 10 },
            { "L", 50 },
            { "C", 100 },
            { "D", 500 },
            { "M", 1000 },
        };

        private string Value { get; }

        public RomanNumber(string value)
        {
            Value = CleanupNumber(value);

        }

        private string CleanupNumber(string value)
        {
            var sb = new StringBuilder();

            foreach (var romanChar in value)
            {
                var romanString = $"{romanChar}".ToUpper();
                if (!_romanValuesMap.ContainsKey(romanString)) continue;
                sb.Append(romanString);
            }

            return sb.ToString();
        }

        public int ToDecimal()
        {
            if (string.IsNullOrWhiteSpace(Value)) return 0;

            var result = 0;
            var accumulator = 0;

            for (var index = 0; index < Value.Length; index++)
            {
                var actual = GetRomanString(index);
                var next = GetRomanString(index + 1);

                accumulator = IsGreaterOrLast(actual, next)
                    ? ResetAccumulatorAndCalculateResult(actual, accumulator, ref result)
                    : accumulator + actual;
            }

            return result;
        }

        private static int ResetAccumulatorAndCalculateResult(int actual, int accumulator, ref int result)
        {
            result += actual - accumulator;
            return 0;
        }

        private static bool IsGreaterOrLast(int actual, int next) => actual >= next || next == 0;

        private int GetRomanString(int index)
        {
            if (index >= Value.Length) return 0;

            var romanChar = Value[index];
            var romanString = $"{romanChar}".ToUpper();
            return _romanValuesMap[romanString];;
        }
    }
}
