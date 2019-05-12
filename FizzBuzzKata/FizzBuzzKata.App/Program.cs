using System;

namespace FizzBuzzKata.App
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("Fizz Buzz Kata");
            var fb = new FizzBuzz();
            for (var i = 1; i <= 100; i++)
            {
                Console.WriteLine($"[{i}]: {fb.GetNumber(i)}");
            }
            Console.ReadKey();
        }
    }
}
