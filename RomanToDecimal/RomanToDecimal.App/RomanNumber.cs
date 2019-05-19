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

        public string Value { get; private set; }

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

                if (IsGreaterOrLast(actual,next))
                {
                    result += actual - accumulator;
                    accumulator = 0;
                }
                else
                {
                    accumulator += actual;
                }
            }

            return result;
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
