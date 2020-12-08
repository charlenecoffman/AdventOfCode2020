using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public class Day5
    {
        private const string _fileName = "./InputFiles/PuzzleInput5.txt";

        public static void Execute()
        {
            Console.WriteLine("**DAY 5**");

            var seatInfo = InputProcessor.FromFileToStringList(_fileName);
            var seats = ParseSeats(seatInfo);
            GetSeatIds(seats);
            var maxSeatId = seats.Max(s => s.SeatId);
            Console.WriteLine($"The highest seat id is {maxSeatId}");
            var mySeat = FindMySeat(seats);
            Console.WriteLine($"My seat id is {mySeat}");
        }

        private static int FindMySeat(IReadOnlyCollection<Seat> seats)
        {
            var minSeat = seats.Min(s => s.SeatId);
            var maxSeat = seats.Max(s => s.SeatId);
            for(var i=minSeat; i<maxSeat; i++)
            {
                if (seats.All(s => s.SeatId != i))
                {
                    return i;
                }
            }

            return 0;
        }

        private static void GetSeatIds(List<Seat> seats)
        {
            var columnMax = 7;
            var rowMax = 127;
            foreach (var seat in seats)
            {
                seat.ColumnNumber = GetNumberFromDirections(seat.ColumnDirections, columnMax);
                seat.RowNumber = GetNumberFromDirections(seat.RowDirections, rowMax);
            }
        }

        private static int GetNumberFromDirections(List<Direction> directions, decimal max)
        {
            decimal min = 0;
            var number = 0;
            var last = directions.Count - 1;

            for (var i=0; i < directions.Count; i++)
            {
                if (directions[i] == Direction.LOWER)
                {
                    if (last == i)
                    {
                        number = (int)min;
                    }
                    max = Math.Floor((max + min) / 2);
                }
                else if (directions[i] == Direction.UPPER)
                {
                    if (last == i)
                    {
                        number = (int)max;
                    }
                    min = Math.Ceiling((max + min) / 2);
                }
            }

            return number;
        }

        private static List<Seat> ParseSeats(List<string> seatInfos)
        {
            var seatList = new List<Seat>();
            foreach (var seatInfo in seatInfos)
            {
                var newSeat = new Seat
                {
                    RowString = seatInfo.Substring(0, seatInfo.Length-3), 
                    ColumnString = seatInfo.Substring(seatInfo.Length-3)
                };
                newSeat.ColumnDirections = ParseColumnDirections(newSeat.ColumnString);
                newSeat.RowDirections = ParseRowDirections(newSeat.RowString);

                seatList.Add(newSeat);
            }

            return seatList;
        }

        private static List<Direction> ParseColumnDirections(string columnString)
        {
            var listOfDirections = new List<Direction>();

            foreach (var dir in columnString)
            {
                if (dir == 'L')
                {
                    listOfDirections.Add(Direction.LOWER);
                }

                if (dir == 'R')
                {
                    listOfDirections.Add(Direction.UPPER);
                }
            }

            return listOfDirections;
        }

        private static List<Direction> ParseRowDirections(string rowString)
        {
            var listOfDirections = new List<Direction>();

            foreach (var dir in rowString)
            {
                if (dir == 'F')
                {
                    listOfDirections.Add(Direction.LOWER);
                }

                if (dir == 'B')
                {
                    listOfDirections.Add(Direction.UPPER);
                }
            }

            return listOfDirections;
        }
    }
}
