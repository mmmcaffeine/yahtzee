using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Dgt.Yahtzee.Engine
{
    public class FullHouseCategoryTests
    {
        [Fact]
        public void Name_Should_BeFullHouse()
        {
            new FullHouseCategory().Name.Should().BeEquivalentTo("full house");
        }

        [Theory]
        [MemberData(nameof(GetGetCategoryScoreTestCases))]
        public void GetCategoryScore_Should_ReturnCategoryScore(IEnumerable<int> scoreValues, int expectedCategoryScore)
        {
            // Arrange
            var sut = new FullHouseCategory();

            // Act
            var categoryScore = sut.GetCategoryScore(scoreValues);

            // Assert
            categoryScore.Should().Be(expectedCategoryScore);
        }

        private static IEnumerable<object[]> GetGetCategoryScoreTestCases()
        {
            yield return new GetCategoryScoreTestCase(new[] { 1, 1, 3, 3 }, 0);         // Less than five dice
            yield return new GetCategoryScoreTestCase(new[] { 1, 1, 1, 3, 3, 3 }, 0);   // More than five dice
            yield return new GetCategoryScoreTestCase(new[] { 1, 2, 3, 4, 5 }, 0);      // Not a combination of two and three of a kind
            yield return new GetCategoryScoreTestCase(new[] { 4, 4, 1, 4, 4 }, 0);      // Not a combination of two and three of a kind
            yield return new GetCategoryScoreTestCase(new[] { 5, 5, 5, 5, 5 }, 0);      // Five of a kind, _not_ two and three of a kind
            yield return new GetCategoryScoreTestCase(new[] { 1, 1, 3, 3, 3 }, 11);     // Two of a kind, and three of a kind
            yield return new GetCategoryScoreTestCase(new[] { 5, 5, 5, 3, 3 }, 21);     // Three of a kind, and two of a kind
            yield return new GetCategoryScoreTestCase(new[] { 2, 4, 2, 2, 4 }, 14);     // Two of a kind, and three of a kind, but not in sequence
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