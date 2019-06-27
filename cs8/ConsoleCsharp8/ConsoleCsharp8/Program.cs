using System;

namespace ConsoleCsharp8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var point1 = new Point()
            {
                X = 1,
                Y = 2
            };

            Console.WriteLine($"point: {point1.ToString()}");

            IMessage msg = new Message
            {
                DisplayMessage = "Hello asp.net core users!"
            };

            Console.WriteLine(msg.GetMessage());

            Console.WriteLine(RockPaperScissors("paper", "scissors"));

        }


        public static string RockPaperScissors(string first, string second)
            => (first, second) switch
            {
                ("rock", "paper") => "rock is covered by paper. Paper wins.",
                ("rock", "scissors") => "rock breaks scissors. Rock wins.",
                ("paper", "rock") => "paper covers rock. Paper wins.",
                ("paper", "scissors") => "paper is cut by scissors. Scissors wins.",
                ("scissors", "rock") => "scissors is broken by rock. Rock wins.",
                ("scissors", "paper") => "scissors cuts paper. Scissors wins.",
                (_, _) => "tie"
            };
    }
}
