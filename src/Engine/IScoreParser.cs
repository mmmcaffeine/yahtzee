using System.Collections.Generic;

namespace Dgt.Yahtzee.Engine
{
    public interface IScoreParser
    {
        IEnumerable<int> GetScoreValues(string score);
        string GetCategoryName(string score);
    }
}