using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day1
    {
        private const string InputFileName = "./InputFiles/PuzzleInput1.txt";

        public static void Execute()
        {
            Console.WriteLine("**DAY 1**");
            var listOfInts = InputProcessor.FromFileToIntList();

            Loop(listOfInts, 3, 2020);
        }

        private static void Loop(List<int> originalList, int maxSums, int goal)
        {
            var theSet = CreateDts(originalList, maxSums);
            var foundIt = false;
            do
            {
                if (!DoesSetSumToGoal(theSet, goal))
                {
                    NextNumber(theSet, originalList.Count);
                }
                else
                {
                    var multiplied = MultipleTheseInts(theSet.Select(s => s.calculatedCurrentValue));
                    Console.WriteLine($"The numbers that add up to {goal} are {string.Join(",", theSet.Select(n => n.calculatedCurrentValue.ToString()).ToArray())}. The total when multiplied is {multiplied}");
                    foundIt = true;
                }
            } while (!foundIt);
        }

        private static int MultipleTheseInts(IEnumerable<int> numberList)
        {
            return numberList.Aggregate(1, (current, number) => current * number);
        }

        private static bool DoesSetSumToGoal(List<IntToSum> theSet, int goal)
        {
            var sum = theSet.Sum(t => t.calculatedCurrentValue);
            return sum == goal;
        }

        private static List<IntToSum> CreateDts(List<int> theOriginalList, int numberOfDts)
        {
            var returnDts = new List<IntToSum>();
            for (var i = 0; i < numberOfDts; i++)
            {
                var newDts = new IntToSum
                {
                    originalList = theOriginalList
                };
                returnDts.Add(newDts);
            }

            return returnDts;
        }

        private static void NextNumber(List<IntToSum> dtsSet, int numberOfNumbers)
        {
            for (var i = dtsSet.Count - 1; i >= 0; i--)
                if (dtsSet[i].currentIndex != numberOfNumbers - 1)
                {
                    dtsSet[i].currentIndex += 1;
                    return;
                }
                else
                {
                    dtsSet[i].currentIndex = 0;
                }
        }

        private void PrintListOfInts(IEnumerable<int> listOfInts)
        {
            foreach (var item in listOfInts) Console.WriteLine(item);
        }
    }
}