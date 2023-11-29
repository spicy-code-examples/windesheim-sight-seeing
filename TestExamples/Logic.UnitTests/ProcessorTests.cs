using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;

namespace Logic.UnitTests
{
    /* This is an example of how AutoFixture and Moq work. */
    [TestClass]
    public class ProcessorTests
    {
        /* In this test class, a global Fixture-instance is created.
         * This ensures that, when a value is generated, no earlier generated value is given - unless all possible options have been exhausted.
         * For example, 0-255 will be generated randomly when requesting bytes.
         * Only after all values have been generated will the pool reset.
         * AutoFixture is smart like that. :) */
        private IFixture _fixture = new Fixture();
        private Mock<IDependency> _dependencyMock = default!;

        /* This piece of code will be executed every time a TestMethod is called. */
        [TestInitialize]
        public void Initialize()
        {
            _fixture.Customize(new AutoMoqCustomization());
            /* The Freeze method might be a bit magical; it is basically a shorthand for creating and injecting a mock.
             * By storing it in a global variable - and having it be reset through a new Freeze for every test - we can call and set it up freely later.
             * For more information, see the following example (or check out our friend Mark Seemann's work directly!)
             * https://stackoverflow.com/questions/18161127/cant-grasp-the-difference-between-freeze-inject-register */
            _dependencyMock = _fixture.Freeze<Mock<IDependency>>();
        }

        /* The test method is structured in a way that describes the case that we're testing.
         * Given a Processor (or a Processor in a particular state, albeit that isn't particularly relevant here)
         * When an Action is Done
         * Then the ExecuteFollowUpAction is called. 
         * On top of that, for every test, we can use the Arrange, Act and Assert set-up to have a uniform set-up. */
        [TestMethod]
        public void Processor_ActionIsDone_ExecuteFollowUpActionIsCalled()
        {
            // Arrange
            Processor subject = _fixture.Create<Processor>();
            /* This will create Processor with randomly generated properties; both the Item and the Dependency values will be randomized.
             * Therefore, the Item can exist in any state; it is not relevant for this test in what state it is, however.
             * By using AutoFixture, we can thus reduce set-up noise. */
            _dependencyMock.Setup(m => m.ExecuteFollowUpActionAsync(subject.Item.Name))
                .Returns(Task.CompletedTask);
                /* This is what a set-up looks like in Moq;
                 * We've basically stated that,
                 * when the ExecuteFollowUpAction is called with the Item in subject and the corresponding ActionCount,
                 * we will override the implementation (for it is irrelevant to this unit we're testing) and return a CompletedTask.
                 * This allows us to control the flow of our test and dictate what external components may or may not do. */

            // Act
            subject.DoAction();

            // Assert
            _dependencyMock.Verify(m => m.ExecuteFollowUpActionAsync(subject.Item.Name), Times.Once());
            _dependencyMock.VerifyNoOtherCalls();
                /* This is a way for us to test that, when DoAction is called on the subject,
                 * the ExecuteFollowUpActionAsync is called using the exact parameters we expect.
                 * We can also verify that no other calls are made to the Dependency. 
                 * This is an example of how Moq works;
                 * allowing us to mock interfaces and external dependencies whose inner workings are irrelevant for this unit of code. */
        }
    }
}
