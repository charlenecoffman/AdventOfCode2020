using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2020
{
    public class Day2
    {
        private const string _fileName = "./InputFiles/PuzzleInput2.txt";

        public static void Execute()
        {
            Console.WriteLine("**DAY 2**");
            var listOfPasswordsAndPoliciesRaw = InputProcessor.FromFileToStringList(_fileName);
            var parsedList = ParsePasswordsFromRawData(listOfPasswordsAndPoliciesRaw);

            var validPasswords = CheckList(parsedList);
            Console.WriteLine(
                $"There are {validPasswords.Count} valid passwords in the database of {listOfPasswordsAndPoliciesRaw.Count} passwords.");
        }

        private static List<Password> CheckList(IEnumerable<Password> passwordList)
        {
            return passwordList.Where(IsValid_TobogganCompany).ToList();
        }

        private static bool IsValid_SledCompany(Password pass)
        {
            var countOfReq = pass.PasswordText.Count(r => r.ToString() == pass.RequiredCharacter);
            return countOfReq >= pass.Min && countOfReq <= pass.Max;
        }


        private static bool IsValid_TobogganCompany(Password pass)
        {
            var inFirstPosition = pass.PasswordText[pass.Min - 1].ToString() == pass.RequiredCharacter;
            var inSecondPosition = pass.PasswordText[pass.Max - 1].ToString() == pass.RequiredCharacter;
            return (inSecondPosition || inFirstPosition) && !(inSecondPosition && inFirstPosition);
        }

        private static IEnumerable<Password> ParsePasswordsFromRawData(
            IEnumerable<string> listOfPasswordsAndPoliciesRaw)
        {
            var newList = new List<Password>();
            foreach (var rawPasswordData in listOfPasswordsAndPoliciesRaw)
            {
                var rawDataSplit = rawPasswordData.Split(":");
                var dashIndex = rawDataSplit[0].IndexOf("-", StringComparison.Ordinal);
                var firstSpace = rawDataSplit[0].IndexOf(" ", StringComparison.Ordinal);
                int.TryParse(rawDataSplit[0].Substring(0, dashIndex), out var min);

                int.TryParse(rawDataSplit[0].Substring(dashIndex + 1, firstSpace - (dashIndex + 1)), out var max);

                var newPassword = new Password
                {
                    PasswordText = rawDataSplit[1].Trim(),
                    Min = min,
                    Max = max,
                    RequiredCharacter = rawDataSplit[0].Substring(firstSpace + 1)
                };

                newList.Add(newPassword);
            }

            return newList;
        }
    }
}