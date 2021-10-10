using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Dgt.Yahtzee.Engine
{
    public class GameScorerTests
    {
        [Fact]
        public void GetRoundScore_Should_DoStuff()
        {
            // Arrange
            const string score = "(1, 6, 2, 6, 1) sixes";
            
            var fakeScoreParser = A.Fake<IScoreParser>();
            A.CallTo(() => fakeScoreParser.GetScoreValues(A<string>.Ignored)).Returns(new[] { 1, 6, 2, 6, 1 });
            A.CallTo(() => fakeScoreParser.GetCategoryName(A<string>.Ignored)).Returns("sixes");
            
            var fakeOnesCategory = A.Fake<ICategory>();
            A.CallTo(() => fakeOnesCategory.Name).Returns("ones");
            A.CallTo(() => fakeOnesCategory.GetCategoryScore(A<IEnumerable<int>>.Ignored)).Returns(2);
            
            var fakeSixesCategory = A.Fake<ICategory>();
            A.CallTo(() => fakeSixesCategory.Name).Returns("sixes");
            A.CallTo(() => fakeSixesCategory.GetCategoryScore(A<IEnumerable<int>>.Ignored)).Returns(12);
            
            var fakeFullHouseCategory = A.Fake<ICategory>();
            A.CallTo(() => fakeFullHouseCategory.Name).Returns("full house");
            A.CallTo(() => fakeFullHouseCategory.GetCategoryScore(A<IEnumerable<int>>.Ignored)).Returns(0);

            var sut = new GameScorer(fakeScoreParser, new[] { fakeOnesCategory, fakeSixesCategory, fakeFullHouseCategory });

            // Act
            var roundScore = sut.GetRoundScore(score);

            // Assert
            roundScore.Should().Be(12);
        }
    }
}