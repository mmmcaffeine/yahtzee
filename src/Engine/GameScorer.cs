using System.Collections.Generic;
using System.Linq;

namespace Dgt.Yahtzee.Engine
{
    public class GameScorer : IGameScorer
    {
        private readonly IScoreParser _scoreParser;
        private readonly IEnumerable<ICategory> _categories;

        // TODO Parameter validation - no nulls, but an empty enumerable is allowed
        public GameScorer(IScoreParser scoreParser, IEnumerable<ICategory> categories)
        {
            _scoreParser = scoreParser;
            _categories = categories;
        }

        // TODO We've assumed we are going to find a category, and that might not be the case. An InvalidOperationException would make sense
        public int GetRoundScore(string score)
        {
            var scoreValues = _scoreParser.GetScoreValues(score);
            var categoryName = _scoreParser.GetCategoryName(score);
            var category = _categories.Single(x => x.Name == categoryName);
            
            return category.GetCategoryScore(scoreValues);
        }
    }
}