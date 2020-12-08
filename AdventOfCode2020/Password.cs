using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{

    public class Password
    {
        public string RequiredCharacter { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public string PasswordText { get; set; }

        public void Print()
        {
            Console.WriteLine($"{Min}-{Max} {RequiredCharacter}: {PasswordText}");
        }

    }
}
