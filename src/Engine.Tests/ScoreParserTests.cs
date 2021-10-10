using System.Linq;
using FluentAssertions;
using Xunit;

namespace Dgt.Yahtzee.Engine
{
    public class ScoreParserTests
    {
        // TODO Add theories for when the ScoreParser is less brittle
        [Fact]
        public void GetScoreValues_Should_ExtractDiceRollsFromScore()
        {
            // Arrange
            const string score = "(1, 1, 2, 4, 4) fours";
            var sut = new ScoreParser();
            
            // Act
            var scoreValues = sut.GetScoreValues(score).ToList();
            
            // Assert
            scoreValues.Should().HaveCount(5).And.Contain(new[] { 1, 1, 2, 4, 4 });
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