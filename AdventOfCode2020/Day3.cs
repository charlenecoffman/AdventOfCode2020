using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day3
    {
        private const string _fileName = "./InputFiles/PuzzleInput3.txt";

        public static void Execute()
        {
            Console.WriteLine("**DAY 3**");

            var rowsInMap = InputProcessor.FromFileToStringList(_fileName);
            var map = ParseLocations(rowsInMap);
            var slopes = GetSlopes();

            var listOfNumberOfTrees = slopes.Select(slope => TraverseTheSlopes(map, slope)).ToList();

            var multipliedTotal = MultipleTheseInts(listOfNumberOfTrees);

            Console.WriteLine($"The number of trees on each slope multiplied together equals {multipliedTotal}");
        }


        private static long MultipleTheseInts(IEnumerable<long> numberList)
        {
            return numberList.Aggregate<long, long>(1, (current, number) => current * number);
        }

        private static long TraverseTheSlopes(IReadOnlyList<List<Location>> map, Slope slope)
        {
            var lastRowIndex = map.Count - 1;
            var currentLocation = map.First().First();

            var atTheBottom = false;
            var numberOfTrees = 0;
            do
            {
                
                if (currentLocation.RowIndex + slope.Down >= lastRowIndex)
                {
                    atTheBottom = true;
                }

                currentLocation = GetNextLocation(currentLocation, map, slope);
                if (currentLocation.Value == '#') numberOfTrees++;

            } while (!atTheBottom);

            return numberOfTrees;
        }

        private static Location GetNextLocation(Location currentLocation, IReadOnlyList<List<Location>> map, Slope slope)
        {
            var newCurrentLocation = new Location
            {
                ColumnIndex = currentLocation.ColumnIndex + slope.Right,
                RowIndex = currentLocation.RowIndex + slope.Down,
                Value = GetMapValueAt(currentLocation.ColumnIndex + slope.Right, currentLocation.RowIndex + slope.Down,
                    map)
            };

            return newCurrentLocation;
        }

        private static char GetMapValueAt(int columnIndex, int rowIndex, IReadOnlyList<List<Location>> map)
        {
            var endOfColumns = map.FirstOrDefault().Count;
            var pretendColumnIndex = columnIndex % endOfColumns;
            return map[rowIndex][pretendColumnIndex].Value;
        }
        private static IEnumerable<Slope> GetSlopes()
        {
            return new List<Slope>
            {
                new Slope {Right = 1, Down = 1},
                new Slope {Right = 3, Down = 1},
                new Slope {Right = 5, Down = 1},
                new Slope {Right = 7, Down = 1},
                new Slope {Right = 1, Down = 2}
            };
        }

        private static void PrintLocation(Location currentLocation)
        {
            Console.WriteLine(
                $"Coordinates of point are ({currentLocation.ColumnIndex},{currentLocation.RowIndex}) and value is {currentLocation.Value}");
        }

        private static List<List<Location>> ParseLocations(IEnumerable<string> rows)
        {
            var map = new List<List<Location>>();
            var rowIterator = 0;
            foreach (var row in rows)
            {
                var mapRow = new List<Location>();
                var columnIterator = 0;
                foreach (var character in row)
                {
                    mapRow.Add(new Location {ColumnIndex = columnIterator, RowIndex = rowIterator, Value = character});
                    columnIterator++;
                }

                map.Add(mapRow);
                rowIterator++;
            }

            return map;
        }
    }
}