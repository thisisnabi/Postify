using NetArchTest.Rules;
using Postify.ArchitectureTests.Base;

namespace Postify.ArchitectureTests.Layers;

public class LayerIsolationTests : BaseTest
{
    [Fact]
    public void Core_Should_Not_Depend_On_Infrastructure()
    {
        // While the .NET compiler prevents circular dependencies between module-specific 
        // Core and Infrastructure projects, this test ensures Core remains independent 
        // of "Postify.Shared.Infrastructure".
        var infraNames = InfrastructureAssemblies.Select(a => a.GetName().Name!).ToArray();

        var result = Types.InAssemblies(CoreAssemblies)
            .ShouldNot()
            .HaveDependencyOnAny(infraNames)
            .GetResult();

        AssertArchResults(result, "The Core layer must remain independent of Infrastructure implementations. Use abstractions instead.");
    }

    [Fact]
    public void Core_Should_Not_Depend_On_ModuleEntry()
    {
        var entryTypes = Types.InAssemblies(ModuleEntryAssemblies)
            .GetTypes()
            .Select(t => t.FullName!)
            .ToArray();

        var result = Types.InAssemblies(CoreAssemblies)
            .ShouldNot()
            .HaveDependencyOnAny(entryTypes)
            .GetResult();

        AssertArchResults(result, "The Core layer should not have dependencies on types in the Module Entry (Presentation/API) layer.");
    }

    [Fact]
    public void Infrastructure_Should_Not_Depend_On_ModuleEntry()
    {
        var entryTypes = Types.InAssemblies(ModuleEntryAssemblies)
            .GetTypes()
            .Select(t => t.FullName!)
            .ToArray();

        var result = Types.InAssemblies(InfrastructureAssemblies)
            .ShouldNot()
            .HaveDependencyOnAny(entryTypes)
            .GetResult();

        AssertArchResults(result, "The Infrastructure layer should not have dependencies on types in the Module Entry (Presentation/API) layer.");
    }

    [Fact]
    public void Core_Should_Not_Depend_On_Presentation_Frameworks()
    {
        var result = Types.InAssemblies(CoreAssemblies)
            .ShouldNot()
            .HaveDependencyOn("Microsoft.AspNetCore")
            .GetResult();

        AssertArchResults(result, "The Core layer should not have dependencies on presentation frameworks (ASP.NET Core).");
    }



    [Fact]
    public void DbContexts_Should_Be_Internal_And_Reside_In_Infrastructure()
    {
        var result = Types.InAssemblies(AllAssemblies)
            .That().Inherit(typeof(Microsoft.EntityFrameworkCore.DbContext))
            .And().DoNotHaveName("ModuleDbContext")
            .Should().NotBePublic()
            .And().ResideInNamespaceEndingWith(".Infrastructure.Persistence")
            .GetResult();
    
        AssertArchResults(result, "Module-specific DbContext implementations must be internal and located within the Infrastructure layer's Persistence namespace.");
    }


    // This ensures all module setup is encapsulated within the Module Entry's extension methods.
    [Fact]
    public void WebApi_Should_Not_Depend_On_Core_Or_Infrastructure_Directly()
    {
        if (WebApiAssembly == null) return;

        var coreNames = CoreAssemblies.Select(a => a.GetName().Name!).ToArray();
        var infraNames = InfrastructureAssemblies.Select(a => a.GetName().Name!).ToArray();

        var result = Types.InAssembly(WebApiAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(coreNames)
            .Or()
            .HaveDependencyOnAny(infraNames)
            .GetResult();

        AssertArchResults(result, "WebApi should only depend on Module Entry projects, not their internal layers. (Core, Infrastructure).");
    }
}