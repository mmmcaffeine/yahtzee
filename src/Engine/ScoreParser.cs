using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dgt.Yahtzee.Engine
{
    public class ScoreParser : IScoreParser
    {
        private const string ScoreValuesPattern = @"(?<=(\(\s*|\s*,\s*))(?<scoreValue>\d+(?=(\s*,\s*|\s*\))))";
        private const string CategoryNamePattern = @"\)\s*(?<categoryName>.+?)\s*$";

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
        public string GetCategoryName(string score)
        {
            return CategoryNameRegex.Match(score).Groups["categoryName"].Value;
        }
    }
}