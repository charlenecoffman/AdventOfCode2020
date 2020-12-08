using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class DigitToSum
    {
        public int currentIndex { get; set; }
        public List<int> originalList { get; set; }
        public int calculatedCurrentValue => originalList[currentIndex];
    }
}
