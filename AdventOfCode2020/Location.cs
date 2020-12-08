using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class Location
    {
        //Left to right
        public int ColumnIndex { get; set; }

        //Up and down
        public int RowIndex { get; set; }
        public char Value { get; set; }
    }
}
