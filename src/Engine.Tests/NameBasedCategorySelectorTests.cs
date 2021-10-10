using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Dgt.Yahtzee.Engine
{
    public class NameBasedCategorySelectorTests
    {
        [Theory]
        [InlineData("threes")]
        [InlineData("THREES")]
        [InlineData("Threes")]
        public void SelectCategory_Should_ReturnCategoryWithMatchingNameIgnoringCase(string categoryName)
        {
            // Arrange
            var fakeOnesCategory = A.Fake<ICategory>();
            A.CallTo(() => fakeOnesCategory.Name).Returns("ones");
            
            var fakeThreesCategory = A.Fake<ICategory>();
            A.CallTo(() => fakeThreesCategory.Name).Returns("threes");
            
            var fakeFullHouseCategory = A.Fake<ICategory>();
            A.CallTo(() => fakeFullHouseCategory.Name).Returns("full house");
            
            var categories = new[] { fakeOnesCategory, fakeThreesCategory, fakeFullHouseCategory };
            var sut = new NameBasedCategorySelector();
            
            // Act
            var category = sut.SelectCategory(null, categoryName, categories);
            
            // Assert
            category.Should().BeSameAs(fakeThreesCategory);
        }

        [Fact]
        public void SelectCategory_Should_ReturnNullWhenNoCategoryHasMatchingName()
        {
            // Arrange
            var fakeOnesCategory = A.Fake<ICategory>();
            A.CallTo(() => fakeOnesCategory.Name).Returns("ones");

            var sut = new NameBasedCategorySelector();
            
            // Act
            var category = sut.SelectCategory(null, "sixes", new[] { fakeOnesCategory });
            
            // Assert
            category.Should().BeNull();
        }
    }
}