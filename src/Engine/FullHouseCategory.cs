using System.Collections.Generic;
using System.Linq;

namespace Dgt.Yahtzee.Engine
{
    public class FullHouseCategory : ICategory
    {
        public string Name => "full house";
        
        // TODO Parameter validation - no nulls
        public int GetCategoryScore(IEnumerable<int> scoreValues)
        {
            var groups = scoreValues.GroupBy(x => x).ToList();
            var correctGrouping = groups.Count == 2
                                  && groups.Sum(group => group.Count()) == 5
                                  && groups.SingleOrDefault(group => group.Count() == 2) is not null;

            return correctGrouping
                ? groups.Sum(group => group.Key * group.Count())
                : 0;
        }
    }
}