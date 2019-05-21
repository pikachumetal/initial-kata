using System;

namespace VendingMachine.App.Exceptions
{
    public class VendingException : Exception
    {
        internal VendingException(VendorErrorEnum errorCode) : base($"{errorCode}") { }
    }
}
