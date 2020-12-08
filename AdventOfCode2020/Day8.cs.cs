using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day8
    {
        private const string _fileName = "./InputFiles/PuzzleInput8.txt";

        public static void Execute()
        {
            Console.WriteLine("**DAY 8**");
            var programRawData = InputProcessor.FromFileToStringList(_fileName);
            var instructions = ParseInstructions(programRawData);
            int accumulator;
            bool failure;
            var lastIndexChanged = -1;
            do
            {
                accumulator = TraverseTheProgram(instructions, 0, out failure);
                ResetTimesVisited(instructions);
                instructions = GetNextInstructionSet(instructions, in lastIndexChanged, out lastIndexChanged);
            } while (failure);

            Console.WriteLine($"The accumlator value is: {accumulator}");
        }

        private static void ResetTimesVisited(IEnumerable<ProgramInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                instruction.TimesVisited = 0;
            }
        }

        private static List<ProgramInstruction> GetNextInstructionSet(List<ProgramInstruction> instructions, in int lastIndexChanged, out int newIndexChanged)
        {
            var iterator = lastIndexChanged;
            if (lastIndexChanged != -1)
            {
                switch (instructions[lastIndexChanged].Instruction)
                {
                    case Instruction.jmp:
                        instructions[lastIndexChanged].Instruction = Instruction.nop;
                        break;
                    case Instruction.nop:
                        instructions[lastIndexChanged].Instruction = Instruction.jmp;
                        break;
                }
            }
            
            for(var i=iterator+1; i < instructions.Count; i++)
            {
                switch (instructions[i].Instruction)
                {
                    case Instruction.jmp:
                        newIndexChanged = i;
                        instructions[i].Instruction = Instruction.nop;
                        return instructions;
                    case Instruction.nop:
                        newIndexChanged = i;
                        instructions[i].Instruction = Instruction.jmp;
                        return instructions;
                }
            }
            
            newIndexChanged = 0;
            return instructions;
        }

        private static int TraverseTheProgram(List<ProgramInstruction> instructions, int index, out bool failure)
        {
            const int accumulator = 0;
            if(index >= instructions.Count)
            {
                failure = false;
                return accumulator;
            }
            var instruction = instructions[index];

            if (instruction.TimesVisited >= 1)
            {
                failure = true;
                return accumulator;
            }

            instruction.TimesVisited++;
            failure = false;
            switch (instruction.Instruction)
            {
                case Instruction.nop:
                    return TraverseTheProgram(instructions, index + 1, out failure);
                case Instruction.acc:
                    return instruction.InstructionNumber + TraverseTheProgram(instructions, index + 1, out failure);
                case Instruction.jmp:
                    return TraverseTheProgram(instructions, index + instruction.InstructionNumber, out failure);
            }

            return accumulator;
        }

        private static List<ProgramInstruction> ParseInstructions(List<string> rawInstructions)
        {
            var instructionList = new List<ProgramInstruction>();

            foreach (var instruct in rawInstructions)
            {
                var splitdata = instruct.Split(" ");

                int.TryParse(splitdata[1], out var instructionNumber);
                Instruction instruction;

                switch (splitdata[0])
                {
                    case "nop":
                        instruction = Instruction.nop;
                        break;
                    case "acc":
                        instruction = Instruction.acc;
                        break;
                    case "jmp":
                        instruction = Instruction.jmp;
                        break;
                    default:
                        instruction = Instruction.nop;
                        break;
                }

                instructionList.Add(new ProgramInstruction
                {
                    Instruction = instruction,
                    InstructionNumber = instructionNumber
                });
            }

            return instructionList;
        }
    }
}