using Xunit;

namespace Dgt.Yahtzee.Engine
{
    public class ScoreParserTests
    {
        [Fact]
        public void Add_Should_ReturnSumOfTwoNumbers()
        {
            Assert.Equal(5, ScoreParser.Add(3, 2));
        }
    }
}