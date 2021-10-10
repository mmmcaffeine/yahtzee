using System.Collections.Generic;
using System.Linq;

namespace Dgt.Yahtzee.Engine
{
    public class FaceValueCategory : ICategory
    {
        public string Name { get; }
        private readonly int _faceValue;

        // TODO Parameter validation of the name - no nulls or empty strings
        public FaceValueCategory(string name, int faceValue)
        {
            Name = name;
            _faceValue = faceValue;
        }

        // TODO Parameter validation - no nulls, but an empty enumerable is reasonable
        public int GetCategoryScore(IEnumerable<int> scoreValues) => scoreValues.Sum(x => x == _faceValue ? _faceValue : 0);
    }
}