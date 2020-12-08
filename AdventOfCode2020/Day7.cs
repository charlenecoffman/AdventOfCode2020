using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2020
{
    public class Day7
    {
        private const string _fileName = "./InputFiles/PuzzleInput7.txt";

        public static void Execute()
        {
            Console.WriteLine("**DAY 7**");
            var rulesRawData = InputProcessor.FromFileToStringList(_fileName);

            var rules = ProcessRules(rulesRawData);
            var bagsAllowed = GetAllowedBags("shiny gold", rules);
            var countOfAllowed = bagsAllowed.Distinct().Count();
            Console.WriteLine($"There are {countOfAllowed} options for the shiny gold bag");
            var bagsRequired = GetRequiredBags(rules.First(r => r.Color == "shiny gold"), rules);
            var count = bagsRequired.Count();
            Console.WriteLine($"There are {count} bags required for the shiny gold bag");
        }

        private static List<string> GetRequiredBags(Rule startingbag, List<Rule> rules)
        {
            var requiredBags = new List<string>();
            var rule = rules.First(r => r.Color == startingbag.Color);

            foreach (var bag in rule.AllowedBags)
            {
                for (var i = 0; i < bag.Amount; i++)
                {
                    requiredBags.Add(bag.Color);
                    requiredBags.AddRange(GetRequiredBags(rules.First(r => r.Color == bag.Color), rules));
                }
                
            }

            return requiredBags;
        }

        private static List<string> GetAllowedBags(string colorToMatch, List<Rule> rules)
        {
            var allowedBags = new List<string>();
            foreach (var rule in rules)
            {
                foreach (var bag in rule.AllowedBags)
                {
                    if (bag.Color == colorToMatch)
                    {
                        if (allowedBags.All(b => b != bag.Color))
                        {
                            allowedBags.Add(rule.Color);
                        }
                    }
                }
            }

            foreach (var color in allowedBags.ToList())
            {
                allowedBags.AddRange(GetAllowedBags(color, rules));
            }

            return allowedBags;
        }

        private static List<Rule> ProcessRules(List<string> rulesRawData)
        {
            var rules = new List<Rule>();
            foreach (var ruleText in rulesRawData)
            {
                var newRule = GetRuleFromRuleText(ruleText);

                rules.Add(newRule);
            }

            return rules;
        }

        private static Rule GetRuleFromRuleText(string ruleText)
        {
            var color = ruleText.Substring(0, ruleText.IndexOf("bags")).Trim();
            var allInnerBags = ruleText.Substring(ruleText.IndexOf("contain")+8);
            var innerBags = new List<InnerBag>();
            if (!allInnerBags.Contains("no other bag"))
            {
                innerBags = ProcessInnerBags(allInnerBags.Replace(".", "").Split(','));
            }
            return new Rule
            {
                Color = color,
                AllowedBags = innerBags
            };
        }

        private static List<InnerBag> ProcessInnerBags(IEnumerable<string> innerBagsText)
        {
            var listOfInnerBags = new List<InnerBag>();

            foreach (var bagText in innerBagsText)
            {
                int.TryParse(Regex.Match(bagText, @"\d").Value, out var number);
                var regex = new Regex( @"\b(?!bags|bag\b)[a-z]+");
                var matches = regex.Matches(bagText);
                var colorArray = matches.Select(m => m.Value);
                var color = string.Join(" ", colorArray);

                listOfInnerBags.Add(new InnerBag
                {
                    Amount = number,
                    Color = color
                });
            }

            return listOfInnerBags;
        }
    }
}