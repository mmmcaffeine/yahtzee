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
                .Select(grouping => (value: grouping.Key, count: grouping.Count() % 2 == 0 ? grouping.Count() : grouping.Count() - 1))
                .ToList();
            
            return pairs.Sum(pair => pair.count) == 4
                ? pairs.Sum(pair => pair.value * pair.count)
                : 0;
        }
    }
}