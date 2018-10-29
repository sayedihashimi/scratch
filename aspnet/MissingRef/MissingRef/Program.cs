
using System;

namespace MissingRef
{
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            JsonConvert foo = null;
        }
    }
}
