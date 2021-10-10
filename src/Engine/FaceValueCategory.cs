using System.Collections.Generic;
using System.Linq;

namespace Dgt.Yahtzee.Engine
{
    public class FaceValueCategory : ICategory
    {
        private int _faceValue = default;

        public FaceValueCategory(int faceValue)
        {
            _faceValue = faceValue;
        }

        // TODO Parameter validation - no nulls, but an empty enumerable is reasonable
        public int GetCategoryScore(IEnumerable<int> scoreValues) => scoreValues.Sum(x => x == _faceValue ? _faceValue : 0);
    }
}