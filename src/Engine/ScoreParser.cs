using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dgt.Yahtzee.Engine
{
    public class ScoreParser : IScoreParser
    {
        private const string ScoreValuesPattern = @"(?<=(\(\s*|\s*,\s*))(?<scoreValue>\d+(?=(\s*,\s*|\s*\))))";
        private const string CategoryNamePattern = @"\) (?<categoryName>.+)$";

        private static readonly Regex ScoreValuesRegex = new(ScoreValuesPattern, RegexOptions.Compiled);
        private static readonly Regex CategoryNameRegex = new(CategoryNamePattern, RegexOptions.Compiled);
        
        // TODO Validate for null and empty strings
        // TODO Throw FormatException if we cannot parse
        public IEnumerable<int> GetScoreValues(string score)
        {
            var matches = ScoreValuesRegex.Matches(score);
            var scoreValues = matches.Select(m => m.Groups["scoreValue"].Value);
            
            return scoreValues.Select(int.Parse);
        }

        // TODO Validate for null and empty strings
        // TODO Throw FormatException if we cannot parse
        // TODO Make more reliable by eliminating the assumptions below
        
        // Assume the input string will be of the form "(1, 1, 2, 4, 4) fours" i.e.
        // * The category name will always start at same point in the string
        // * The category name will always go to the end of the string
        public string GetCategoryName(string score)
        {
            return CategoryNameRegex.Match(score).Groups["categoryName"].Value;
        }
    }
}