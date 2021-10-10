using System.Collections.Generic;

namespace Dgt.Yahtzee.Engine
{
    public class ScoreParser : IScoreParser
    {
        public IEnumerable<int> GetScoreValues(string score)
        {
            throw new System.NotImplementedException("The method or operation is not implemented.");
        }

        public static int Add(int x, int y) => x + y;
    }
}