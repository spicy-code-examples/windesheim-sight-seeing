using AutoFixture;
using AutoFixture.AutoMoq;

namespace Logic.UnitTests
{
    /* In order to further reduce repeated noise in tests, we use the following in our TestClasses.
     * This allows us to inherit from TestFixture (or in specific TestFixture<TSubject>).
     * What this piece of code does, is automatically create and expose an instance of both Fixture and Subject to use in the test.
     * Given that every test requires you to globally set a Fixture instance as well as define a Subject for every individual test
     * abstracting that behind this class emphasizes what it is we want to test. 
     *
     * If we were to use this in ItemTests or ProcessorTests, we would state the TestClass as follows:
     * [TestClass]
     * Item(or Processor)Tests : TestFixture<Item(or Processor)>
     *
     * Now, Subject will give us an instance of what we specified as TSubject (which is Item or Processor)
     * as well as the Fixture instance with a default AutoMoqCustomization, allowing us to instantly use Fixture.Freeze for mocks.
     * 
     * Feel free to rewrite the ItemTests or ProcessorTests using this class! */
    [TestClass]
    public abstract class TestFixture<TSubject> : TestFixture
        where TSubject : notnull
    {
        protected TSubject Subject { get; set; } = default!;

        [TestInitialize]
        public override void Initialize()
        {
            /* This will call the TestFixture abstract class (without the subject, below) 
             * and give us an instance of Fixture.
             * This can be done in case we are trying to, for example,
             * test a static class that cannot be instantiated by AutoFixture. */
            base.Initialize();
            Subject = Fixture.Create<TSubject>();
        }
    }

    [TestClass]
    public abstract class TestFixture
    {
        protected IFixture Fixture { get; set; } = default!;

        [TestInitialize]
        public virtual void Initialize()
        {
            /* This AutoMoqCustomization allows us to instantly call Fixture.Freeze on mocks
             * and have them be injected automatically. */
            Fixture = new Fixture().Customize(new AutoMoqCustomization());
        }
    }
}
