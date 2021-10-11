using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dgt.Yahtzee.Engine
{
    public class ScoreParser : IScoreParser
    {
        private const string ScoreValuesPattern = @"(?<=(\(|, ))(?<scoreValue>\d+(?=(, |\))))";

        private readonly Regex _scoreValuesRegEx = new(ScoreValuesPattern, RegexOptions.Compiled);
        
        // TODO Validate for null and empty strings
        // TODO Throw FormatException if we cannot parse
        // TODO Make more reliable by eliminating the assumptions below
        
        // Assume the input string will be of the form "(1, 1, 2, 4, 4) fours" i.e.
        // * Will always start with an open paren, and no whitespace
        // * Will always be six-sided die (i.e. the value will be between 1 and 6 and therefore a single digit)
        // * Dice rolls will always be separated by a comma followed by a space
        // * Dice rolls will always end with a close paren
        public IEnumerable<int> GetScoreValues(string score)
        {
            var matches = _scoreValuesRegEx.Matches(score);
            var scoreValues = matches.Select(m => m.Groups["scoreValue"].Value);
            
            return scoreValues.Select(int.Parse);
        }

        // TODO Validate for null and empty strings
        // TODO Replace with Regex parsing
        // TODO Throw FormatException if we cannot parse
        // TODO Make more reliable by eliminating the assumptions below
        
        // Assume the input string will be of the form "(1, 1, 2, 4, 4) fours" i.e.
        // * The category name will always start at same point in the string
        // * The category name will always go to the end of the string
        public string GetCategoryName(string score)
        {
            return score[16..];
        }
    }
}