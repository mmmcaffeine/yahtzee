using System.Collections.Generic;

namespace Dgt.Yahtzee.Engine
{
    public interface ICategorySelector
    {
        ICategory? SelectCategory(IEnumerable<int>? scoreValues, string? categoryName, IEnumerable<ICategory> categories);
    }
}