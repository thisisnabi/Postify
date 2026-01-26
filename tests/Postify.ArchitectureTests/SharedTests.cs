using NetArchTest.Rules;

namespace Postify.ArchitectureTests;

public class SharedTests : BaseTest
{
    [Fact]
    public void SharedCore_Should_Not_Depend_On_SharedInfrastructure()
    {
        var sharedCore = SharedAssemblies.Where(a => a.GetName().Name?.EndsWith(".Core") == true).ToArray();
        var sharedInfra = SharedAssemblies.Where(a => a.GetName().Name?.EndsWith(".Infrastructure") == true).ToArray();

        var infraNames = sharedInfra.Select(a => a.GetName().Name!).ToArray();

        var result = Types.InAssemblies(sharedCore)
            .ShouldNot()
            .HaveDependencyOnAny(infraNames)
            .GetResult();

        AssertArchResults(result, "Shared.Core should not depend on Shared.Infrastructure.");
    }

    [Fact]
    public void SharedKernel_Should_Not_Depend_On_Other_Shared_Projects()
    {
        var sharedKernel = SharedAssemblies.Where(a => a.GetName().Name?.EndsWith(".Kernel") == true).ToArray();
        var otherSharedNames = SharedAssemblies
            .Where(a => a.GetName().Name?.EndsWith(".Kernel") == false)
            .Select(a => a.GetName().Name!)
            .ToArray();

        var result = Types.InAssemblies(sharedKernel)
            .ShouldNot()
            .HaveDependencyOnAny(otherSharedNames)
            .GetResult();

        AssertArchResults(result, "Shared.Kernel should be the most basic project and not depend on other Shared projects.");
    }
}
