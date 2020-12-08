using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020
{
    public class ProgramInstruction
    {
        public Instruction Instruction { get; set; }
        public int InstructionNumber { get; set; }
        public int TimesVisited { get; set; }

    }

    public enum Instruction
    {
        nop,
        acc,
        jmp
    }
}
