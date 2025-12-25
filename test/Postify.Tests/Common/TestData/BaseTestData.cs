using AutoFixture;
using AutoFixture.Kernel;

namespace Postify.Profile.Tests.Common.TestData;

public abstract class BaseTestData
{
    protected Fixture Fixture { get; }

    protected BaseTestData()
    {
        Fixture = new Fixture();
        Fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        Fixture.Behaviors.Add(new OmitOnRecursionBehavior());
    }
}

