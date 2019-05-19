using RomanToDecimal.App;
using Xunit;

namespace RomanToDecimal.Test
{
    public class ConverterTest
    {
        [Fact]
        public void Test_When_EmptyString_Then_ReturnZero()
        {
            var roman = new RomanNumber("");
            var result = roman.ToDecimal();
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_When_IncorrectValues_Then_ReturnZero()
        {
            var roman = new RomanNumber("QWERTYUOPASFGHJKÑZBN");
            var result = roman.ToDecimal();
            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_When_I_Then_One()
        {
            var roman = new RomanNumber("I");
            var result = roman.ToDecimal();
            Assert.Equal(1, result);
        }

        [Fact]
        public void Test_When_V_Then_5()
        {
            var roman = new RomanNumber("V");
            var result = roman.ToDecimal();
            Assert.Equal(5, result);
        }

        [Fact]
        public void Test_When_X_Then_10()
        {
            var roman = new RomanNumber("X");
            var result = roman.ToDecimal();
            Assert.Equal(10, result);
        }

        [Fact]
        public void Test_When_L_Then_50()
        {
            var roman = new RomanNumber("L");
            var result = roman.ToDecimal();
            Assert.Equal(50, result);
        }

        [Fact]
        public void Test_When_C_Then_100()
        {
            var roman = new RomanNumber("C");
            var result = roman.ToDecimal();
            Assert.Equal(100, result);
        }

        [Fact]
        public void Test_When_D_Then_500()
        {
            var roman = new RomanNumber("D");
            var result = roman.ToDecimal();
            Assert.Equal(500, result);
        }

        [Fact]
        public void Test_When_M_Then_1000()
        {
            var roman = new RomanNumber("M");
            var result = roman.ToDecimal();
            Assert.Equal(1000, result);
        }

        [Fact]
        public void Test_When_ValuesAreGreaterToLower_Then_SumAll()
        {
            var roman = new RomanNumber("MDCLXVI");
            var result = roman.ToDecimal();
            Assert.Equal(1666, result);
        }

        [Fact]
        public void Test_When_LowerChars_Then_SumAll()
        {
            var roman = new RomanNumber("mdclxvi");
            var result = roman.ToDecimal();
            Assert.Equal(1666, result);
        }

        [Fact]
        public void Test_When_IV_Then_4()
        {
            var roman = new RomanNumber("IV");
            var result = roman.ToDecimal();
            Assert.Equal(4, result);
        }

        [Fact]
        public void Test_When_MCMXLIV_Then_1944()
        {
            var roman = new RomanNumber("MCMXLIV");
            var result = roman.ToDecimal();
            Assert.Equal(1944, result);
        }
    }
}
