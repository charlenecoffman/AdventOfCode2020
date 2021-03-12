using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public class Day9
    {
        private const string _fileName = "./InputFiles/PuzzleInput9.txt";

        public static void Execute()
        {
            Console.WriteLine("**DAY 9**");
            var numberList = InputProcessor.FromFileToLongList(_fileName);
            long theBrokenOne = 0;
            const int preamble = 25;


            for (var i = preamble; i < numberList.Count; i++)
            {
                var currentPossibleIndexes = GetIndexesOfPossibleSumNumbers(preamble, i, numberList);
                var indexesThatMatch = FindMatchingIndexesThatSum(currentPossibleIndexes, numberList, i);
                if (indexesThatMatch.Count >= 2) continue;
                theBrokenOne = numberList[i];
                Console.WriteLine($"Found the broken one: {theBrokenOne}");
                break;
            }

            var encryptionWeakness = GetEncryptionWeakness(numberList.Where(l => l != theBrokenOne).ToList(), theBrokenOne);
            Console.WriteLine($"The encrypton weakness is {encryptionWeakness}");
            
        }

        private static long GetEncryptionWeakness(List<long> numberList, long number)
        {
            long weakness = 0;
            var rangeLength = 2;

            for (var i = 0; i < numberList.Count; i++)
            {
                if (i + rangeLength > numberList.Count - 1)
                {
                    rangeLength++;
                    i = -1;
                }
                else
                {
                    var numbersToSum = GetNextNumbers(i, rangeLength, numberList);
                    var sum = numbersToSum.Sum();
                    if (sum != number) continue;
                    weakness = numbersToSum.Min() + numbersToSum.Max();
                    break;
                }
            }


            return weakness;
        }

        private static List<long> GetNextNumbers(int indexToStart, int rangeLength, List<long> numberList)
        {
            var listOfNumbers = new List<long>();
            for (var i = indexToStart; i < indexToStart + rangeLength; i++)
            {
                listOfNumbers.Add(numberList[i]);
            }

            return listOfNumbers;
        }

        private static List<int> FindMatchingIndexesThatSum(IEnumerable<int> currentPossibleIndexes, IReadOnlyList<long> numberList, int matchingIndex)
        {
            var numberToMatch = numberList[matchingIndex];
            var possibleIndexes = currentPossibleIndexes.ToList();
            foreach (var firstIndex in possibleIndexes)
            {
                var firstNumber = numberList[firstIndex];
                
                foreach (var secondIndex in possibleIndexes)
                {
                    var secondNumber = numberList[secondIndex];
                    if (firstIndex != secondIndex && firstNumber != secondNumber && secondNumber + firstNumber == numberToMatch)
                    {
                        return new List<int>{firstIndex, secondIndex};
                    }
                }
            }

            return new List<int>();
        }

        private static IEnumerable<int> GetIndexesOfPossibleSumNumbers(int preamble, int index, ICollection numberList)
        {
            var newList = new List<int>();
            var takeNumberIterator = 0;
            for (var i = index-preamble; i < numberList.Count && takeNumberIterator < preamble; i++)
            {
                newList.Add(i);
                takeNumberIterator++;
            }

            return newList;
        }
    }
}
