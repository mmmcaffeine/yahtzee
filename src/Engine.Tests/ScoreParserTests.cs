using FluentAssertions;
using Xunit;

namespace Dgt.Yahtzee.Engine
{
    public class ScoreParserTests
    {
        [Fact]
        public void Add_Should_ReturnSumOfTwoNumbers()
        {
            ScoreParser.Add(3, 2).Should().Be(5);
        }
    }
}