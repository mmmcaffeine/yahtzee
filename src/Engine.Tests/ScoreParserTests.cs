using System.Collections.Generic;
using System.Linq;
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
            var scoreValues = sut.GetScoreValues(score).ToList();
            
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