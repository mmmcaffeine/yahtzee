using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Dgt.Yahtzee.Engine
{
    public class ScoreParser : IScoreParser
    {
        private const string ScoreValuesSplitPattern = @"\s*\(\s*|\s*,\s*|\s*\).*";
        private const string CategoryNameCapturePattern = @"\)\s*(?<categoryName>.+?)\s*$";

        private static readonly Regex ScoreValuesRegex = new(ScoreValuesSplitPattern, RegexOptions.Compiled);
        private static readonly Regex CategoryNameRegex = new(CategoryNameCapturePattern, RegexOptions.Compiled);
        
        // TODO Validate for null and empty strings
        // TODO Throw FormatException if we cannot parse
        public IEnumerable<int> GetScoreValues(string score)
        {
            // Behaviour of the Split method is to include empty strings if consecutive delimiters are found, or if delimiters
            // are found at the start or end of the input string. Although we might not expect the former, we definitely
            // expect the latter
            var results = ScoreValuesRegex.Split(score);
            var scoreValues = results.Where(result => !string.IsNullOrWhiteSpace(result));
            
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