using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020
{
    public class Day6
    {
        private const string _fileName = "./InputFiles/PuzzleInput6.txt";

        public static void Execute()
        {
            Console.WriteLine("**DAY 6**");
            var customsAnswers = InputProcessor.FromFileToStringList(_fileName);
            var passangersAnswers = ParsePassengerAnswers(customsAnswers);
            var answerCounts = GetAnswerCounts(passangersAnswers);

            Console.WriteLine($"The total sum of all answers is {answerCounts.Sum()}");
        }

        private static IEnumerable<int> GetAnswerCounts(IEnumerable<Group> passangersAnswers)
        {
            var answerCounts = new List<int>();

            foreach (var answer in passangersAnswers)
            {
                answerCounts.Add(answer.answerCount.Count(a => a.Value == answer.count));
            }

            return answerCounts;
        }

        private static List<Group> ParsePassengerAnswers(List<string> customsAnswers)
        {
            var allAnswers = new List<Group>();
            var answerGroup = new Group{answerCount = new Dictionary<char, int>()};
            var countInGroup = 0;

            for (var i = 0; i < customsAnswers.Count; i++)
            {
                countInGroup++;
                if (customsAnswers[i] == "" || i == customsAnswers.Count - 1)
                {
                    if (customsAnswers[i] == "")
                    {
                        answerGroup.count = countInGroup - 1;
                        countInGroup = 0;
                    }
                    if (i == customsAnswers.Count - 1)
                    {
                        answerGroup.count = countInGroup;
                        AddCharactersToGroup(answerGroup, customsAnswers[i]);
                    }
                    allAnswers.Add(answerGroup);
                    answerGroup = new Group{answerCount = new Dictionary<char, int>()};
                }
                else
                {
                    AddCharactersToGroup(answerGroup, customsAnswers[i]);
                }
            }

            return allAnswers;
        }

        private static void AddCharactersToGroup(Group answerGroup, string customsAnswer)
        {
            foreach (var character in customsAnswer)
            {
                if (answerGroup.answerCount.ContainsKey(character))
                {
                    answerGroup.answerCount[character] = answerGroup.answerCount[character] + 1;
                }
                else
                {
                    answerGroup.answerCount.Add(character, 1);
                }
            }
        }
    }
}
