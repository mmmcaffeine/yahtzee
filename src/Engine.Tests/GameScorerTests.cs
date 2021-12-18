using System;
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

        // TODO Enhance the exception check
        // * It should indicate the score in some way
        // * It should indicate the categories in some way
        // * It should indicate the category selector in some way
        // It might make sense to put these in Data if we don't want to build a custom exception type
        // It might make sense to suffix these onto Message if we don't want to build a custom exception type
        [Fact]
        public void GetRoundScore_Should_ThrowWhenNoCategoryIsSelected()
        {
            // Arrange
            var fakeScoreParser = A.Fake<IScoreParser>();
            var fakeCategorySelector = A.Fake<ICategorySelector>();

            A.CallTo(() => fakeCategorySelector.SelectCategory(An<IEnumerable<int>>._, A<string>._, An<IEnumerable<ICategory>>._))
                .Returns(null);

            var sut = new GameScorer(fakeScoreParser, Array.Empty<ICategory>(), fakeCategorySelector);

            // Act, Assert
            sut.Invoking(x => x.GetRoundScore("(1, 2, 1, 3, 5) ones"))
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().StartWith("The score for the round did not match any of the available categories.");
        }

        // TODO Enhance the exception check
        // * It should indicate the score in some way
        // * It should indicate the categories in some way, specifically the category we tried to play again
        // * It should indicate the category selector in some way
        // It might make sense to put these in Data if we don't want to build a custom exception type
        // It might make sense to suffix these onto Message if we don't want to build a custom exception type
        [Fact]
        public void GetRoundScore_Should_NotAllowSameCategoryToBeUsedMoreThanOnce()
        {
            // Arrange
            var fakeScoreParser = A.Fake<IScoreParser>();
            A.CallTo(() => fakeScoreParser.GetCategoryName(A<string>._)).Returns("full house");
            
            var fakeCategory = A.Fake<ICategory>();
            A.CallTo(() => fakeCategory.Name).Returns("full house");

            var fakeCategorySelector = A.Fake<ICategorySelector>();
            A.CallTo(() => fakeCategorySelector.SelectCategory(An<IEnumerable<int>>._, "full house", An<IEnumerable<ICategory>>._))
                .Returns(fakeCategory);

            var sut = new GameScorer(fakeScoreParser, new[] { fakeCategory }, fakeCategorySelector);
            _ = sut.GetRoundScore("(3, 3, 5, 5, 5) full house");
            
            // Act, Assert
            sut.Invoking(x => x.GetRoundScore("(1, 1, 6, 6, 6) full house"))
                .Should().Throw<InvalidOperationException>()
                .And.Message.Should().StartWith("The score for the round tried to use a category that has already been used.");
        }
    }
}