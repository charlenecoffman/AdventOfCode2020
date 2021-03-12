using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    class IntToSum
    {
        public int currentIndex { get; set; }
        public List<int> originalList { get; set; }
        public int calculatedCurrentValue => originalList[currentIndex];
    }
    class LongToSum
    {
        public int currentIndex { get; set; }
        public List<long> originalList { get; set; }
        public long calculatedCurrentValue => originalList[currentIndex];
    }
}
