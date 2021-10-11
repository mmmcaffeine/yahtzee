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
            yield return new object[] { "(6) ones", new[] { 6 } };
            yield return new object[] { "(3, 5) threes", new[] { 3, 5 } };
            yield return new object[] { "(1, 1, 2, 4, 4) fours", new[] { 1, 1, 2, 4, 4 } };
            yield return new object[] { "(6, 5, 3, 3, 5, 1) chance", new[] { 6, 5, 3, 3, 5, 1} };
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