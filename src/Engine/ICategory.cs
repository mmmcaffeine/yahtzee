using System.Collections.Generic;

namespace Dgt.Yahtzee.Engine
{
    public interface ICategory
    {
        string Name { get; }
        int GetCategoryScore(IEnumerable<int> scoreValues);
    }
}