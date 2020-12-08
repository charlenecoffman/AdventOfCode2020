using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day4
    {
        private const string _fileName = "./InputFiles/PuzzleInput4.txt";

        public static void Execute()
        {
            Console.WriteLine("**DAY 4**");

            var passportRows = InputProcessor.FromFileToStringList(_fileName);
            var combineRowCollections = ParseGroupsOfData(passportRows);
            var passports = ParsePassports(combineRowCollections);

            var validPassportCount = GetNumberOfValidPassports(passports);

            Console.WriteLine($"In the batch of {passports.Count} passports there were {validPassportCount} valid passports");
        }

        private static int GetNumberOfValidPassports(IEnumerable<Passport> passports)
        {
            var count = 0;

            foreach(var passport in passports)
            {
                var isPassportValid = ValidateYear(passport.BirthYear, 1920, 2002) &&
                                      ValidateYear(passport.ExpirationYear, 2020, 2030) &&
                                      ValidateEyeColor(passport.EyeColor) &&
                                      ValidateHairColor(passport.HairColor) &&
                                      ValidateHeight(passport.Height) &&
                                      ValidateYear(passport.IssueYear, 2010, 2020) &&
                                      ValidatePassportId(passport.PassportId);

                if(isPassportValid)
                {
                    count++;
                }
            }

            return count;
        }

        private static bool ValidatePassportId(string passportId)
        {
            if(string.IsNullOrEmpty(passportId)) return false;
            if(!long.TryParse(passportId, out _)) return false;

            return passportId.Length == 9;
        }

        private static bool ValidateEyeColor(string eyeColor)
        {
            if (string.IsNullOrEmpty(eyeColor)) return false;

            var colorOptions = new List<string> {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"};

            return colorOptions.Contains(eyeColor.ToLower());
        }

        private static bool ValidateHairColor(string hairColor)
        {
            if (string.IsNullOrEmpty(hairColor)) return false;
            if (hairColor[0] != '#') return false;

            var colorCode = hairColor.Substring(1);

            if (colorCode.Length != 6) return false;

            var match = Regex.Match(colorCode, @"[A-Za-z0-9]*");

            return match.Success;
        }

        private static bool ValidateHeight(string height)
        {
            if (string.IsNullOrEmpty(height)) return false;

            var unit = height.Substring(height.Length - 2);
            var number = height.Substring(0, height.Length - 2);

            if (unit != "cm" && unit != "in") return false;

            var min = 0;
            var max = 0;

            
            var isNumber = int.TryParse(number, out var heightAsNumber);
            if (!isNumber) return false;

            switch (unit)
            {
                case "cm":
                    min = 150;
                    max = 193;
                    break;
                case "in":
                    min = 59;
                    max = 76;
                    break;
            }

            return heightAsNumber >= min && heightAsNumber <= max;
        }

        private static bool ValidateYear(string year, int min, int max)
        {
            if (string.IsNullOrEmpty(year)) return false;

            if (year.Length != 4) return false;

            var isYearInt = int.TryParse(year, out var yearAsInt);

            if (!isYearInt) return false;

            return yearAsInt >= min && yearAsInt <= max;
        }

        private static IEnumerable<string> ParseGroupsOfData(IEnumerable<string> passportRows)
        {
            var collections = new List<string>();
            var stringBuilder = new StringBuilder();

            foreach (var passportRow in passportRows)
            {
                if (string.IsNullOrEmpty(passportRow))
                {
                    collections.Add(stringBuilder.ToString());
                    stringBuilder = new StringBuilder();
                }
                else
                {
                    stringBuilder.Append($" {passportRow}");
                }
            }
            
            return collections;
        }

        private static List<Passport> ParsePassports(IEnumerable<string> passportCollectionRows)
        {
            var passports = new List<Passport>();

            foreach (var passport in passportCollectionRows)
            {
                var newPassport = new Passport();
                var dictionary = ParsePassportRowAsDictionary(passport);
                dictionary.TryGetValue("byr", out var birthYear);
                newPassport.BirthYear = birthYear;
                dictionary.TryGetValue("iyr", out var issueYear);
                newPassport.IssueYear = issueYear;
                dictionary.TryGetValue("eyr", out var expirationYear);
                newPassport.ExpirationYear = expirationYear;
                dictionary.TryGetValue("hgt", out var height);
                newPassport.Height = height;
                dictionary.TryGetValue("hcl", out var hairColor);
                newPassport.HairColor = hairColor;
                dictionary.TryGetValue("ecl", out var eyeColor);
                newPassport.EyeColor = eyeColor;
                dictionary.TryGetValue("pid", out var passportId);
                newPassport.PassportId = passportId;
                dictionary.TryGetValue("cid", out var countryId);
                newPassport.CountryId = countryId;
                passports.Add(newPassport);
            }

            return passports;
        }

        private static Dictionary<string, string> ParsePassportRowAsDictionary(string passport)
        {
            var dictionary = new Dictionary<string, string>();
            var regex = new Regex(@"[\w]*:[A-Za-z0-9#]*");

            foreach (Match match in regex.Matches(passport))
            {
                var set = match.ToString().Split(":");
                dictionary.Add(set[0], set[1]);
            }

            return dictionary;
        }
    }
}