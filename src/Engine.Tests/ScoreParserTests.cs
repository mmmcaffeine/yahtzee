using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace Dgt.Yahtzee.Engine
{
    public class ScoreParserTests
    {
        // TODO Add theories for when the ScoreParser is less brittle
        [Theory]
        [MemberData(nameof(GetGetScoreValuesData))]
        public void GetScoreValues_Should_ExtractDiceRollsFromScore(string score, IEnumerable<int> expected)
        {
            // Arrange
            var sut = new ScoreParser();
            
            // Act
            var scoreValues = sut.GetScoreValues(score).ToList();
            
            // Assert
            scoreValues.Should().BeEquivalentTo(expected);
        }

        private static IEnumerable<object[]> GetGetScoreValuesData()
        {
            // Parse any number of dice rolls
            yield return new object[] { "(6) ones", new[] { 6 } };
            yield return new object[] { "(3, 5) threes", new[] { 3, 5 } };
            yield return new object[] { "(1, 1, 2, 4, 4) fours", new[] { 1, 1, 2, 4, 4 } };
            yield return new object[] { "(6, 5, 3, 3, 5, 1) chance", new[] { 6, 5, 3, 3, 5, 1} };
            
            // Parse things other than six-side dice e.g. we can work with a D20
            yield return new object[] { "(10, 12, 7) tens", new[] { 10, 12, 7 } };
            yield return new object[] { "(4, 15, 9) yahtzee", new[] { 4, 15, 9 } };
            
            // Parse things even when we have superfluous whitespace or commas
            yield return new object[] { "    (   10  ,   17, 7   )    pair", new[] { 10, 17, 7 } };
            yield return new object[] { "(5, , , 4)", new[] { 5, 4 } };
        }

        // TODO Add theories for when the ScoreParser is less brittle
        [Fact]
        public void GetCategoryName_Should_ExtractCategoryNameFromScore()
        {
            // Arrange
            const string score = "(1, 1, 2, 4, 4) fours";
            var sut = new ScoreParser();
            
            // Act
            var categoryName = sut.GetCategoryName(score);
            
            // Assert
            categoryName.Should().BeEquivalentTo("fours");
        }
    }
}