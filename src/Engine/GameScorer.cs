using System.Collections.Generic;
using System.Linq;

namespace Dgt.Yahtzee.Engine
{
    public class GameScorer : IGameScorer
    {
        private readonly IScoreParser _scoreParser;
        private readonly IEnumerable<ICategory> _categories;
        private readonly ICategorySelector _categorySelector;

        // TODO Parameter validation - no nulls, but an empty enumerable is allowed
        public GameScorer(IScoreParser scoreParser, IEnumerable<ICategory> categories, ICategorySelector categorySelector)
        {
            _scoreParser = scoreParser;
            _categories = categories;
            _categorySelector = categorySelector;
        }

        // TODO We've assumed we are going to find a category, and that might not be the case. An InvalidOperationException would make sense
        public int GetRoundScore(string score)
        {
            var scoreValues = _scoreParser.GetScoreValues(score).ToList();
            var categoryName = _scoreParser.GetCategoryName(score);
            var category = _categorySelector.SelectCategory(scoreValues, categoryName, _categories);
            
            return category!.GetCategoryScore(scoreValues);
        }
    }
}