using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Dgt.Yahtzee.Engine
{
    public class TwoPairsCategoryTests
    {
        [Fact]
        public void Name_Should_BeTwoPairs()
        {
            new FullHouseCategory().Name.Should().BeEquivalentTo("full house");
        }
        
        [Theory]
        [MemberData(nameof(GetGetCategoryScoreTestCases))]
        public void GetCategoryScore_Should_ReturnSumOfPairsOfDice(IEnumerable<int> scoreValues, int expectedCategoryScore)
        {
            // Arrange
            var sut = new TwoPairsCategory();

            // Act
            var categoryScore = sut.GetCategoryScore(scoreValues);

            // Assert
            categoryScore.Should().Be(expectedCategoryScore);
        }
        
        private static IEnumerable<object[]> GetGetCategoryScoreTestCases()
        {
            yield return new GetCategoryScoreTestCase(new[] { 1, 1, 3, 3 }, 0);         // Two pairs, but less than five dice
            yield return new GetCategoryScoreTestCase(new[] { 1, 1, 3, 3, 5, 6 }, 0);   // Twp pairs, but more than five dice
            yield return new GetCategoryScoreTestCase(new[] { 1, 2, 3, 4, 5 }, 0);      // No pairs
            yield return new GetCategoryScoreTestCase(new[] { 1, 1, 3, 4, 5 }, 0);      // Only one pair
            yield return new GetCategoryScoreTestCase(new[] { 2, 2, 4, 4, 5 }, 12);     // Two pairs of different face values
            yield return new GetCategoryScoreTestCase(new[] { 4, 4, 1, 4, 4 }, 16);     // Two pairs of the same face values
            yield return new GetCategoryScoreTestCase(new[] { 4, 1, 3, 4, 3 }, 14);     // Two pairs, but not in sequence 
            yield return new GetCategoryScoreTestCase(new[] { 1, 1, 3, 3, 3 }, 8);      // Full house, but still has two pairs!
            yield return new GetCategoryScoreTestCase(new[] { 5, 5, 5, 5, 5 }, 20);     // Yahtzee, but still has two pairs!
        }
        
        private class GetCategoryScoreTestCase
        {
            private IEnumerable<int> ScoreValues { get; }
            private int ExpectedCategoryScore { get; }
            
            public GetCategoryScoreTestCase(IEnumerable<int> scoreValues, int expectedCategoryScore)
            {
                ScoreValues = scoreValues;
                ExpectedCategoryScore = expectedCategoryScore;
            }

            public static implicit operator object[](GetCategoryScoreTestCase testCase) =>
                new object[] { testCase.ScoreValues, testCase.ExpectedCategoryScore };
        }
    }
}