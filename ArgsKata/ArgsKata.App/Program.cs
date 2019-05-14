using System;
using System.Collections.Generic;

namespace ArgsKata.App
{
    internal class Program
    {
        private static void Main(params string[] parameters)
        {
            Console.WriteLine("ArgsKata Kata");
            var args = new Args();
            args.LoadSchema("b,c*,v#");
            args.Parse(new List<string>() );
            args.GetString("c");
            Console.ReadKey();
        }
    }
}
