using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgsKata.App.Enumerations;

namespace ArgsKata.App.Exceptions
{
    public class SchemaException:Exception
    {
        public SchemaException(SchemaErrorEnum errorCode):base($"{errorCode}")
        {
            
        }
    }
}
