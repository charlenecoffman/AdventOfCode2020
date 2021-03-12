using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020
{
    public static class InputProcessor
    {
        private const string _fileName = "./InputFiles/PuzzleInput1.txt";
        public static List<int> FromFileToIntList(string fileName = _fileName)
        {
            var fileContents = File.ReadLines(fileName);
            var listOfInts = new List<int>();

            foreach (var line in fileContents)
            {
                if (!int.TryParse(line, out var result))
                {
                    Console.WriteLine("This could not be converted to an int: " + line);
                }
                else
                {
                    listOfInts.Add(result);
                }
            }

            return listOfInts;
        }
        public static List<string> FromFileToStringList(string fileName = _fileName)
        {
            var fileContents = File.ReadLines(fileName);
            return fileContents.ToList();
        }
        public static List<long> FromFileToLongList(string fileName = _fileName)
        {
            var fileContents = File.ReadLines(fileName);
            var listOfInts = new List<long>();

            foreach (var line in fileContents)
            {
                if (!long.TryParse(line, out var result))
                {
                    Console.WriteLine("This could not be converted to an int: " + line);
                }
                else
                {
                    listOfInts.Add(result);
                }
            }

            return listOfInts;
        }
    }
}
