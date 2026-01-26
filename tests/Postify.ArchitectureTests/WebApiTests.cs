using NetArchTest.Rules;

namespace Postify.ArchitectureTests;

public class WebApiTests : BaseTest
{
    [Fact]
    public void WebApi_Should_Not_Depend_On_Core_Directly()
    {
        if (WebApiAssembly == null) return;

        var coreNames = CoreAssemblies.Select(a => a.GetName().Name!).ToArray();

        var result = Types.InAssembly(WebApiAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(coreNames)
            .GetResult();

        AssertArchResults(result, "WebApi should only depend on Module Entry projects, not their internal Core layers. All module setup must be encapsulated within the Module Entry's extension methods.");
    }

    [Fact]
    public void WebApi_Should_Not_Depend_On_Infrastructure_Directly()
    {
        if (WebApiAssembly == null) return;

        var infraNames = InfrastructureAssemblies.Select(a => a.GetName().Name!).ToArray();

        var result = Types.InAssembly(WebApiAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(infraNames)
            .GetResult();

        AssertArchResults(result, "WebApi should only depend on Module Entry projects, not their internal Infrastructure layers. All module setup must be encapsulated within the Module Entry's extension methods.");
    }
}
