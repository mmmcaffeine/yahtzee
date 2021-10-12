using System.Collections.Generic;
using System.Linq;

namespace Dgt.Yahtzee.Engine
{
    public class TwoPairsCategory : ICategory
    {
        public string Name => "two pairs";
        
        // TODO Parameter validation - no nulls
        public int GetCategoryScore(IEnumerable<int> scoreValues)
        {
            var groupings = scoreValues.GroupBy(x => x).ToList();
            
            if (groupings.Sum(x => x.Count()) != 5)
            {
                return 0;
            }
            
            var pairs = groupings
                .Select(CreatePair)
                .ToList();

            return pairs.Sum(pair => pair.Count) == 4
                ? pairs.Sum(pair => pair.FaceValue * pair.Count)
                : 0;
            
            (int FaceValue, int Count) CreatePair(IGrouping<int, int> grouping) =>
                (grouping.Key, grouping.Count() % 2 == 0 ? grouping.Count() : grouping.Count() - 1);
        }
    }
}