using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public class Seat
    {
        public string ColumnString { get; set; }
        public string RowString { get; set; }
        public List<Direction> ColumnDirections { get; set; }
        public List<Direction> RowDirections { get; set; }
        public int RowNumber { get; set; }
        
        public int ColumnNumber { get; set; }

        public int SeatId => (RowNumber * 8) + ColumnNumber;
    }

    public enum Direction
    {
        UPPER,
        LOWER
    }
}
