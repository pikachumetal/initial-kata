using System;
using FizzBuzzKata.App;
using Xunit;

namespace FizzBuzzKata.Test
{
    public class FizzBuzzTest
    {
        private readonly FizzBuzz _fb = new FizzBuzz();

        [Fact]
        public void FizzBuzz_GetNumber_WhenNumber_ShowNumber()
        {
            var result = _fb.GetNumber(1);
            Assert.Equal("1", result);
        }

        [Fact]
        public void FizzBuzz_GetNumber_WhenNumberIsLesserThanOne_ThrowException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _fb.GetNumber(0));
        }

        [Fact]
        public void FizzBuzz_GetNumber_WhenNumberIsGreaterThanOneHundred_ThrowException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _fb.GetNumber(101));
        }

        [Fact]
        public void FizzBuzz_GetNumber_WhenNumberIsThree_ShowFizz()
        {
            var result = _fb.GetNumber(3);
            Assert.Equal("Fizz", result);
        }

        [Fact]
        public void FizzBuzz_GetNumber_WhenNumberIsDivisibleByThree_ShowFizzExceptIsDivisibleByFive()
        {
            for (var i = 1; i < 34; i++)
            {
                if (i % 5 == 0) continue;
                var result = _fb.GetNumber(i * 3);
                Assert.Equal("Fizz", result);
            }
        }

        [Fact]
        public void FizzBuzz_GetNumber_WhenNumberIsFive_ShowFuzz()
        {
            var result = _fb.GetNumber(5);
            Assert.Equal("Fuzz", result);
        }

        [Fact]
        public void FizzBuzz_GetNumber_WhenNumberIsDivisibleByFive_ShowFuzzExceptIsDivisibleByThree()
        {
            for (var i = 1; i < 21; i++)
            {
                if (i % 3 == 0) continue;
                var result = _fb.GetNumber(i * 5);
                Assert.Equal("Fuzz", result);
            }
        }

        [Fact]
        public void FizzBuzz_GetNumber_WhenNumberIsDivisibleByThreeAndFive_ShowFizzFuzz()
        {
            for (var i = 1; i < 101; i++)
            {
                if (i % 3 > 0 || i % 5 > 0 ) continue;
                var result = _fb.GetNumber(i);
                Assert.Equal("FizzFuzz", result);
            }
        }
    }
}
