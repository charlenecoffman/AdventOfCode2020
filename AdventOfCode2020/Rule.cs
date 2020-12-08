using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public class Rule
    {
        public string Color { get; set; }
        public List<InnerBag> AllowedBags { get; set; }
    }

    public class InnerBag
    {
        public string Color { get; set; }
        public int Amount { get; set; }
    }
}
