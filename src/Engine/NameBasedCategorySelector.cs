using System;
using System.Collections.Generic;
using System.Linq;

namespace Dgt.Yahtzee.Engine
{
    public class NameBasedCategorySelector : ICategorySelector
    {
        // TODO Parameter validation - no null categories, but an empty enumerable is okay
        public ICategory? SelectCategory(IEnumerable<int>? scoreValues, string? categoryName, IEnumerable<ICategory> categories)
        {
            return categories.SingleOrDefault(x => string.Equals(x.Name, categoryName, StringComparison.OrdinalIgnoreCase));
        }
    }
}