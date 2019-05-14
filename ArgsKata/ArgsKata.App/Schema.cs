using System.Collections.Generic;
using ArgsKata.App.Enumerations;
using ArgsKata.App.Exceptions;

namespace ArgsKata.App
{
    public class Schema
    {
        private readonly Dictionary<string, SchemaTypeEnum> _strategyType = new Dictionary<string, SchemaTypeEnum>
            {
                {"", SchemaTypeEnum.Boolean},
                {"#", SchemaTypeEnum.Integer},
                {"*", SchemaTypeEnum.String}
            };

        public char Key { get; private set; }
        public SchemaTypeEnum Type { get; private set; }

        public Schema(string parameter)
        {
            AssertParametersAreNoEmpty(parameter);
            Key = parameter[0];

            var stringType = (parameter.Length > 1) ? parameter.Substring(1) : "";
            AssertParameterIsValid(stringType);
            Type = _strategyType[stringType];
        }

        private  void AssertParameterIsValid(string parameter)
        {
            if (!_strategyType.ContainsKey(parameter)) throw new SchemaException(SchemaErrorEnum.ParameterNonValid);
        }

        private static void AssertParametersAreNoEmpty(string parameter)
        {
            if (parameter.Length <= 0) throw new SchemaException(SchemaErrorEnum.ParameterEmpty);
        }
    }
}
