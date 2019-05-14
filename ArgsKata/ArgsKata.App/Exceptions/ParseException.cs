using ArgsKata.App.Enumerations;
using System;

namespace ArgsKata.App.Exceptions
{
    public class ParseException : Exception
    {
        public ParseException(ParseErrorEnum errorCode) : base($"{errorCode}")
        {

        }
    }
}
