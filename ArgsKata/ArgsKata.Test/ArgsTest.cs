using ArgsKata.App;
using ArgsKata.App.Enumerations;
using ArgsKata.App.Exceptions;
using System;
using System.Collections.Generic;
using Xunit;

namespace ArgsKata.Test
{
    public class ArgsTest
    {
        private readonly Args _args;
        public ArgsTest()
        {
            _args = new Args();
        }

        [Fact]
        public void When_NoArgs__Then_ReturnEmpty()
        {
            _args.LoadSchema(string.Empty);
            _args.Parse(Array.Empty<string>());
            var numberArguments = _args.NumberArguments;

            Assert.Equal(0, numberArguments);
        }

        [Fact]
        public void When_TwoArgsEmptyDef__Then_Exception()
        {
            var ex = Assert.Throws<SchemaException>(() => _args.LoadSchema(","));
            Assert.Equal($"{SchemaErrorEnum.ParameterEmpty}", ex.Message);
        }

        [Fact]
        public void When_OneArgKeyIsMoreThanOneChar__Then_Exception()
        {
            var ex = Assert.Throws<SchemaException>(() => _args.LoadSchema("aa"));
            Assert.Equal($"{SchemaErrorEnum.ParameterNonValid}", ex.Message);
        }

        [Fact]
        public void When_OneArgKeyParameterNonValid__Then_Exception()
        {
            var ex = Assert.Throws<SchemaException>(() => _args.LoadSchema("a~"));
            Assert.Equal($"{SchemaErrorEnum.ParameterNonValid}", ex.Message);
        }

        [Fact]
        public void When_SchemaArgDuplicate__ThenException()
        {
            var ex = Assert.Throws<SchemaException>(() => _args.LoadSchema("b,b"));
            Assert.Equal($"{SchemaErrorEnum.ParameterDuplicate}", ex.Message);
        }

        [Fact]
        public void When_SchemaKeyIsNumeric__Then_Exception()
        {
            var ex = Assert.Throws<SchemaException>(() => _args.LoadSchema("1"));
            Assert.Equal($"{SchemaErrorEnum.ParameterKeyNumeric}", ex.Message);
        }

        [Fact]
        public void When_SchemaBoolAndNoArgument__Then_ReturnDefaultBoolean()
        {
            _args.LoadSchema("b");
            _args.Parse(new[] { "-b" });
            var argument = _args.GetBoolean("b");

            Assert.False(argument);
        }

        [Fact]
        public void When_SchemaBoolAndGetDifferentType__Then_Exception()
        {
            _args.LoadSchema("b");
            _args.Parse(new[] { "-b" });

            var ex = Assert.Throws<ParseException>(() => _args.GetInteger("b"));
            Assert.Equal($"{ParseErrorEnum.InvalidType}", ex.Message);
        }

        [Fact]
        public void When_SchemaIntegerAndGetDifferentType__Then_Exception()
        {
            _args.LoadSchema("i#");
            _args.Parse(new[] { "-i" });

            var ex = Assert.Throws<ParseException>(() => _args.GetString("i"));
            Assert.Equal($"{ParseErrorEnum.InvalidType}", ex.Message);
        }

        [Fact]
        public void When_SchemaStringAndGetDifferentType__Then_Exception()
        {
            _args.LoadSchema("s*");
            _args.Parse(new[] { "-s" });

            var ex = Assert.Throws<ParseException>(() => _args.GetBoolean("s"));
            Assert.Equal($"{ParseErrorEnum.InvalidType}", ex.Message);
        }
        
        [Fact]
        public void When_SchemaBooleanAndNoArgumentNorKey__Then_ReturnDefaultBoolean()
        {
            _args.LoadSchema("b");
            _args.Parse(new List<string>());
            var argument = _args.GetBoolean("b");

            Assert.False(argument);
        }

        [Fact]
        public void When_SchemaBoolAndTrue__Then_ReturnTrue()
        {
            _args.LoadSchema("b");
            _args.Parse(new[] { "-b", "true" });
            var argument = _args.GetBoolean("b");

            Assert.True(argument);
        }

        [Fact]
        public void When_SchemaKeyNotExistInArguments__Then_Exception()
        {
            _args.LoadSchema("b");

            var ex = Assert.Throws<ParseException>(() => _args.Parse(new[] { "-c" }));
            Assert.Equal($"{ParseErrorEnum.SchemaNotContainsKey}", ex.Message);
        }

        [Fact]
        public void When_SchemaBoolAndNonBoolValue__Then_Exception()
        {
            _args.LoadSchema("b");

            var ex = Assert.Throws<ParseException>(() => _args.Parse(new[] { "-b", "1" }));
            Assert.Equal($"{ParseErrorEnum.NonBooleanValue}", ex.Message);
        }

        [Fact]
        public void When_SchemaNumericAndNoArgument__Then_ReturnDefaultInteger()
        {
            _args.LoadSchema("i#");
            _args.Parse(new[] { "-i" });
            var argument = _args.GetInteger("i");

            Assert.Equal(0, argument);
        }

        [Fact]
        public void When_SchemaNumericAndNoArgumentNorKey__Then_ReturnDefaultInteger()
        {
            _args.LoadSchema("i#");
            _args.Parse(new List<string>());
            var argument = _args.GetInteger("i");

            Assert.Equal(0, argument);
        }

        [Fact]
        public void When_SchemaNumericAnd10__Then_Return10()
        {
            _args.LoadSchema("i#");
            _args.Parse(new[] { "-i", "10" });
            var argument = _args.GetInteger("i");

            Assert.Equal(10, argument);
        }

        [Fact]
        public void When_SchemaNumericAndNonNumericValue__Then_Exception()
        {
            _args.LoadSchema("i#");

            var ex = Assert.Throws<ParseException>(() => _args.Parse(new[] { "-i", "as" }));
            Assert.Equal($"{ParseErrorEnum.NonIntegerValue}", ex.Message);
        }

        [Fact]
        public void When_SchemaStringAndNoArgument__Then_ReturnDefaultString()
        {
            _args.LoadSchema("s*");
            _args.Parse(new[] { "-s" });
            var argument = _args.GetString("s");

            Assert.Equal("", argument);
        }

        [Fact]
        public void When_SchemaStringAndNoArgumentNorKey__Then_ReturnDefaultString()
        {
            _args.LoadSchema("s*");
            _args.Parse(new List<string>());
            var argument = _args.GetString("s");

            Assert.Equal("", argument);
        }

        [Fact]
        public void When_FullSchemaEmptyArguments__Then_ReturnValidData()
        {
            _args.LoadSchema(" b , s*, i #");
            _args.Parse(new List<string>());
            _args.GetString("s");

            Assert.False(_args.GetBoolean("b"));
            Assert.Equal(0, _args.GetInteger("i"));
            Assert.Equal("", _args.GetString("s"));
        }

        [Fact]
        public void When_FullSchemaFullArguments__Then_ReturnValidData()
        {
            _args.LoadSchema(" b , s*, i #");
            _args.Parse(new[] { "-s", "-pepito", "-b", "true", "-i", "-25"  });
            _args.GetString("s");

            Assert.True(_args.GetBoolean("b"));
            Assert.Equal(-25, _args.GetInteger("i"));
            Assert.Equal("-pepito", _args.GetString("s"));
        }

        [Fact]
        public void When_FlagArgumentAndValueArgument__Then_ReturnValidData()
        {
            _args.LoadSchema(" b , s*, i #");
            _args.Parse(new[] { "-s", "-b", "true", "-i", "-25"  });
            _args.GetString("s");

            Assert.True(_args.GetBoolean("b"));
            Assert.Equal(-25, _args.GetInteger("i"));
            Assert.Equal("", _args.GetString("s"));
        }
    }
}
