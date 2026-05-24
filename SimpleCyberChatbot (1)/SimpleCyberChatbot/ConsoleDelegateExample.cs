using System;

namespace ConsoleDelegateExample
{
    internal class Program
    {
        // A delegate is a variable that stores a method.
        public delegate string SimpleDelegate();

        static void Main(string[] args)
        {
            SimpleDelegate myDelegate = SayHello;

            string result = myDelegate();

            Console.WriteLine(result);
            Console.ReadLine();
        }

        static string SayHello()
        {
            return "Hello students. This is a simple delegate example.";
        }
    }
}
