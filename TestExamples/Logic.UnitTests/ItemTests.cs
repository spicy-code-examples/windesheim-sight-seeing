using AutoFixture;
using FluentAssertions;

namespace Logic.UnitTests
{
    /* This is an example of how AutoFixture and FluentAssertions work. */
    [TestClass]
    public class ItemTests
    {
        /* In this test class, a global Fixture-instance is created.
         * This ensures that, when a value is generated, no earlier generated value is given - unless all possible options have been exhausted.
         * For example, 0-255 will be generated randomly when requesting bytes.
         * Only after all values have been generated will the pool reset.
         * AutoFixture is smart like that. :) */
        private readonly IFixture _fixture = new Fixture();

        /* The test method is structured in a way that describes the case that we're testing.
         * Given an Item with a particular ActionCount
         * When an Action is Performed
         * Then the ActionCount is incremented by one.
         * On top of that, for every test, we can use the Arrange, Act and Assert set-up to have a uniform set-up. */
        [TestMethod]
        public void ItemWithActionCount_ActionIsPerformed_ActionCountIsIncrementedByOne()
        {
            // Arrange
            Item subject = _fixture.Create<Item>();
                /* This will create Item with randomly generated properties; both the name and the ActionCount value will be randomized. */
            var initialActionCount = subject.ActionCount;
                /* Therefore, this ActionCount value can be any randomly generated integer. */

            // Act
            subject.PerformAction();

            // Assert
            subject.ActionCount.Should().Be(initialActionCount + 1);
                /* We don't really care whether the initial count is 1, 2 or 500 -
             * all we want to test is that, regardless of the value, it is incremented by 1.
             * Also, this is an example of the FluentAssertions-syntax. */
        }
    }
}
