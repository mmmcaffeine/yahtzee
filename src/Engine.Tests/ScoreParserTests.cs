using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace Dgt.Yahtzee.Engine
{
    public class ScoreParserTests
    {
        [Theory]
        [MemberData(nameof(GetGetScoreValuesData))]
        public void GetScoreValues_Should_ExtractDiceRollsFromScore(string score, IEnumerable<int> expected)
        {
            // Arrange
            var sut = new ScoreParser();
            
            // Act
            var scoreValues = sut.GetScoreValues(score);
            
            // Assert
            scoreValues.Should().BeEquivalentTo(expected);
        }
        
        private static IEnumerable<object[]> GetGetScoreValuesData()
        {
            // Parse any number of dice rolls
            yield return new GetScoreValuesTestCase("(6) ones", new[] { 6 });
            yield return new GetScoreValuesTestCase("(3, 5) threes", new[] { 3, 5 });
            yield return new GetScoreValuesTestCase("(1, 1, 2, 4, 4) fours", new[] { 1, 1, 2, 4, 4 });
            yield return new GetScoreValuesTestCase("(6, 5, 3, 3, 5, 1) chance", new[] { 6, 5, 3, 3, 5, 1});
            
            // Parse things other than six-side dice e.g. we can work with a D20
            yield return new GetScoreValuesTestCase("(10, 12, 7) tens", new[] { 10, 12, 7 });
            yield return new GetScoreValuesTestCase("(4, 15, 9) yahtzee", new[] { 4, 15, 9 });
            
            // Parse things even when we have superfluous whitespace or commas
            yield return new GetScoreValuesTestCase("    (   10  ,   17, 7   )    pair", new[] { 10, 17, 7 });
            yield return new GetScoreValuesTestCase("(5, , , 4) ones", new[] { 5, 4 });
        }

        private class GetScoreValuesTestCase
        {
            private string Score { get; }
            private IEnumerable<int> ExpectedScoreValues { get; }

            public GetScoreValuesTestCase(string score, IEnumerable<int> expectedScoreValues)
            {
                Score = score;
                ExpectedScoreValues = expectedScoreValues;
            }

            public static implicit operator object[](GetScoreValuesTestCase data)
            {
                return new object[] { data.Score, data.ExpectedScoreValues };
            }
        }

        [Theory]
        [MemberData(nameof(GetGetCategoryNameData))]
        public void GetCategoryName_Should_ExtractCategoryNameFromScore(string score, string expectedCategoryName)
        {
            // Arrange
            var sut = new ScoreParser();
            
            // Act
            var categoryName = sut.GetCategoryName(score);
            
            // Assert
            categoryName.Should().BeEquivalentTo(expectedCategoryName);
        }

        private static IEnumerable<object[]> GetGetCategoryNameData()
        {
            yield return new GetCategoryNameTestCase("(1, 1, 2, 4, 4) fours", "fours");
            yield return new GetCategoryNameTestCase("(5, 5, 3, 1, 6)     chance", "chance");
            yield return new GetCategoryNameTestCase("(6, 1, 3, 1, 5) yahtzee     ", "yahtzee");
            yield return new GetCategoryNameTestCase("(3, 3, 1, 6, 5) full house", "full house");
        }

        private class GetCategoryNameTestCase
        {
            private string Score { get; }
            private string ExpectedCategoryName { get; }
            
            public GetCategoryNameTestCase(string score, string expectedCategoryName)
            {
                Score = score;
                ExpectedCategoryName = expectedCategoryName;
            }
            
            public static implicit operator object[](GetCategoryNameTestCase testCase)
            {
                return new object[] { testCase.Score, testCase.ExpectedCategoryName };
            }
        }
    }
}