using System;
using ArgsKata.App.Enumerations;
using ArgsKata.App.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ArgsKata.App
{
    public class Args
    {
        private const string SchemaSeparator = ",";

        private const string KeyPrefix = "-";
        private const string StringTrue = "true";
        private const string StringFalse = "false";

        public int NumberArguments { get; private set; } = 0;
        private readonly IDictionary<string, SchemaTypeEnum> _schemas = new Dictionary<string, SchemaTypeEnum>();
        private readonly IDictionary<string, object> _values = new Dictionary<string, object>();
        
        public void LoadSchema(string schemas)
        {
            if (string.IsNullOrWhiteSpace(schemas)) return;

            var schemaParameters = schemas.Replace(" ", "").Split(SchemaSeparator);
            foreach (var schemaParameter in schemaParameters)
            {
                var schema = new Schema(schemaParameter);
                if (!char.IsLetter(schema.Key)) throw new SchemaException(SchemaErrorEnum.ParameterKeyNumeric);
                if (_schemas.ContainsKey(schema.Key.ToString())) throw new SchemaException(SchemaErrorEnum.ParameterDuplicate);
                _schemas.Add(schema.Key.ToString(), schema.Type);
            }
        }


        public void Parse(ICollection<string> arguments)
        {
            if (!arguments.Any()) return;
            var list = arguments.ToArray();

            for (var index = 0; index < list.Length; index++)
            {
                var key = GetKeyFromArguments(list, index);
                if (!_schemas.ContainsKey(key)) throw new ParseException(ParseErrorEnum.SchemaNotContainsKey);

                index = ParseKey(key, index, list);
            }
        }
        private static string GetKeyFromArguments(IReadOnlyList<string> list, int index)
        {
            var key = GetElementKeyFromArguments(list, index).Replace(KeyPrefix, "");
            return key.Trim();
        }

        private int ParseKey(string key, int index, IReadOnlyList<string> list)
        {
            var schemaType = _schemas[key];
            object value;
            int nextIndex;

            switch (schemaType)
            {
                case SchemaTypeEnum.Boolean:
                    value = ParseBoolean(list, index, out nextIndex);
                    break;
                case SchemaTypeEnum.Integer:
                    value = ParseInteger(list, index, out nextIndex);
                    break;
                case SchemaTypeEnum.String:
                    value = ParseString(list, index, out nextIndex);
                    break;
                default:
                    throw new SchemaException(SchemaErrorEnum.ParameterNonValid);
            }

            _values.Add(key, value);
            index = nextIndex;
            return index;
        }

        private static string ParseString(IReadOnlyList<string> list, int index, out int nextIndex)
        {
            nextIndex = index + 1;
            var rawValue = GetElementKeyFromArguments(list, nextIndex) ?? "";

            if (!IsOnlyFlag(rawValue)) return rawValue;

            nextIndex = index;
            return "";

        }

        private static int ParseInteger(IReadOnlyList<string> list, int index, out int nextIndex)
        {
            nextIndex = index + 1;
            var rawValue = GetElementKeyFromArguments(list, nextIndex) ?? "";

            if (IsOnlyFlag(rawValue))
            {
                nextIndex = index;
                return default;
            }

            if (!TryParseInteger(rawValue, out var value)) throw new ParseException(ParseErrorEnum.NonIntegerValue);
            return value;
        }

        private static bool TryParseInteger(string rawValue, out int value) => int.TryParse(rawValue, out value);

        private static bool ParseBoolean(IReadOnlyList<string> list, int index, out int nextIndex)
        {
            nextIndex = index + 1;
            var rawValue = GetElementKeyFromArguments(list, nextIndex) ?? "";

            if (IsOnlyFlag(rawValue))
            {
                nextIndex = index;
                return default;
            }

            if (!TryParseBool(rawValue, out var value)) throw new ParseException(ParseErrorEnum.NonBooleanValue);
            return value;
        }

        private static bool TryParseBool(string rawValue, out bool value)
        {
            value = false;
            if (string.IsNullOrWhiteSpace(rawValue)) return false;
            if (!IsBooleanValue(rawValue)) return false;

            value = rawValue.Trim() == StringTrue;
            return true;
        }

        private static bool IsBooleanValue(string rawValue) => rawValue.Trim() == StringTrue || rawValue.Trim() == StringFalse;

        private static bool IsOnlyFlag(string rawValue)
        {
            if (string.IsNullOrWhiteSpace(rawValue)) return true;
            if (rawValue.Length == 1) return false;
            if (rawValue.Length > 2) return false;
            return rawValue.StartsWith(KeyPrefix) && !char.IsNumber(rawValue, 1);
        }

        private static string GetElementKeyFromArguments(IReadOnlyList<string> list, int index)
        {
            if (list.Count <= index) return string.Empty;
            var key = list[index];
            return key.Trim();
        }


        public bool GetBoolean(string key)
        {
            if (_schemas.ContainsKey(key) && !_values.ContainsKey(key)) return default;
            if (_schemas[key] != SchemaTypeEnum.Boolean) throw new ParseException(ParseErrorEnum.InvalidType);
            var rawValue = _values[key] ?? false;
            return (bool)rawValue;
        }

        public int GetInteger(string key)
        {
            if (_schemas.ContainsKey(key) && !_values.ContainsKey(key)) return default;
            if (_schemas[key] != SchemaTypeEnum.Integer) throw new ParseException(ParseErrorEnum.InvalidType);
            var rawValue = _values[key] ?? 0;
            return (int)rawValue;
        }

        public IEnumerable<char> GetString(string key)
        {
            if (_schemas.ContainsKey(key) && !_values.ContainsKey(key)) return "";
            if (_schemas[key] != SchemaTypeEnum.String) throw new ParseException(ParseErrorEnum.InvalidType);
            var rawValue = _values[key] ?? "";
            return (string)rawValue;
        }
    }
}
