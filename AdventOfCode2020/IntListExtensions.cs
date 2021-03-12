using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public static class IntListExtensions
    {
        public static List<int> TakeFromSpecifiedIndex(this List<int> originalList, int takeNumber, int index)
        {
            var newList = new List<int>();
            var takeNumberIterator = 0;
            for (var i = index; i < originalList.Count && takeNumberIterator < takeNumber; i++)
            {
                newList.Add(originalList[i]);
                takeNumberIterator++;
            }

            return newList;
        }
    }
}
