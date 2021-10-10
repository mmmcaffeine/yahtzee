using FluentAssertions;
using Xunit;

namespace Dgt.Yahtzee.Engine
{
    public class FaceValueCategoryTests
    {
        [Fact]
        public void GetCategoryScore_Should_ReturnZeroWhenNoScoreValuesMatchFaceValue()
        {
            // Arrange
            var scoreValues = new[] { 3, 5, 1, 3, 6 };
            var sut = new FaceValueCategory(2);
            
            // Act
            var categoryScore = sut.GetCategoryScore(scoreValues);
            
            // Assert
            categoryScore.Should().Be(0);
        }

        [Fact]
        public void GetCategoryScore_Should_ReturnSumOfScoreValuesMatchingFaceValue()
        {
            // Arrange
            var scoreValues = new[] { 3, 5, 1, 3, 6 };
            var sut = new FaceValueCategory(3);
            
            // Act
            var categoryScore = sut.GetCategoryScore(scoreValues);
            
            // Assert
            categoryScore.Should().Be(6);
        }
    }
}