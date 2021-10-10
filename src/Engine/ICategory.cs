using System.Collections.Generic;

namespace Dgt.Yahtzee.Engine
{
    public interface ICategory
    {
        public int GetCategoryScore(IEnumerable<int> scoreValues);
    }
}