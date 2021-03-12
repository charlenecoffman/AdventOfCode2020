using System;
using System.Diagnostics;

namespace AdventOfCode2020
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            Console.WriteLine("This is Charlene Coffman's Advent Of Code attempts for 2020'!");

            Day1.Execute();

            Day2.Execute();

            Day3.Execute();

            Day4.Execute();

            Day5.Execute();

            Day6.Execute();

            Day7.Execute();

            Day8.Execute();

            Day9.Execute();

            Console.WriteLine("----The End----");
            
            stopWatch.Stop();

            var ts = stopWatch.Elapsed;
            var elapsedTime = $"{ts.Hours:00}:{ts.Minutes:00}:{ts.Seconds:00}.{ts.Milliseconds / 10:00}";

            Console.WriteLine("All programs finished in " + elapsedTime);
        }
    }
}
