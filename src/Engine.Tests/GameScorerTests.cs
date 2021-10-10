using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Dgt.Yahtzee.Engine
{
    public class GameScorerTests
    {
        [Fact]
        public void GetRoundScore_Should_ReturnScoreForSelectedCategory()
        {
            // Arrange
            const string score = "(1, 6, 2, 6, 1) sixes";
            var scoreValues = new[] { 1, 6, 2, 6, 1 };
            const string categoryName = "sixes";
            
            var fakeScoreParser = A.Fake<IScoreParser>();
            A.CallTo(() => fakeScoreParser.GetScoreValues(A<string>._)).Returns(scoreValues);
            A.CallTo(() => fakeScoreParser.GetCategoryName(A<string>._)).Returns(categoryName);
            
            var fakeOnesCategory = A.Fake<ICategory>();
            A.CallTo(() => fakeOnesCategory.Name).Returns("ones");
            A.CallTo(() => fakeOnesCategory.GetCategoryScore(An<IEnumerable<int>>._)).Returns(2);
            
            var fakeSixesCategory = A.Fake<ICategory>();
            A.CallTo(() => fakeSixesCategory.Name).Returns("sixes");
            A.CallTo(() => fakeSixesCategory.GetCategoryScore(An<IEnumerable<int>>._)).Returns(12);
            
            var fakeCategories = new[] { fakeOnesCategory, fakeSixesCategory };
            var fakeCategorySelector = A.Fake<ICategorySelector>();
            A.CallTo(() => fakeCategorySelector.SelectCategory(An<IEnumerable<int>>.That.IsSameSequenceAs(scoreValues), categoryName, fakeCategories))
                .Returns(fakeSixesCategory);
            
            var sut = new GameScorer(fakeScoreParser, fakeCategories, fakeCategorySelector);
            
            // Act
            var roundScore = sut.GetRoundScore(score);
            
            // Assert
            roundScore.Should().Be(12);
        }
    }
}