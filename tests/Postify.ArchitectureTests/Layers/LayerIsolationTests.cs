using NetArchTest.Rules;
using Postify.ArchitectureTests.Base;

namespace Postify.ArchitectureTests.Layers;

public class LayerIsolationTests : BaseTest
{
    [Fact]
    public void Core_Should_Not_Depend_On_Infrastructure()
    {
        // Core should be independent of all infrastructure implementations.
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
        // Core should not depend on the Module Entry layer (Controllers/Extensions).
        // We check for dependencies on types belonging to the Entry assemblies.
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
        // Infrastructure should be independent of the Module Entry layer.
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
        // Ensures the Core layer is decoupled from ASP.NET Core / Web concerns.
        var result = Types.InAssemblies(CoreAssemblies)
            .ShouldNot()
            .HaveDependencyOn("Microsoft.AspNetCore")
            .GetResult();

        AssertArchResults(result, "The Core layer should not have dependencies on presentation frameworks (ASP.NET Core).");
    }

    [Fact]
    public void DbContexts_Should_Be_Internal_And_Reside_In_Infrastructure()
    {
        // DbContext implementations are an Infrastructure concern and should not be public.
        var result = Types.InAssemblies(AllAssemblies)
            .That().Inherit(typeof(Microsoft.EntityFrameworkCore.DbContext))
            .And().DoNotHaveName("ModuleDbContext")
            .Should().NotBePublic()
            .And().ResideInNamespaceEndingWith(".Infrastructure.Persistence")
            .GetResult();

        AssertArchResults(result, "Module-specific DbContext implementations must be internal and located within the Infrastructure layer's Persistence namespace.");
    }

    [Fact]
    public void WebApi_Should_Not_Depend_On_Core_Or_Infrastructure_Directly()
    {
        // WebApi should only depend on Module Entry projects, not their internal layers.
        // This ensures all module setup is encapsulated within the Module Entry's extension methods.
        if (WebApiAssembly == null) return;

        var coreNames = CoreAssemblies.Select(a => a.GetName().Name!).ToArray();
        var infraNames = InfrastructureAssemblies.Select(a => a.GetName().Name!).ToArray();

        var result = Types.InAssembly(WebApiAssembly)
            .ShouldNot()
            .HaveDependencyOnAny(coreNames)
            .Or()
            .HaveDependencyOnAny(infraNames)
            .GetResult();

        AssertArchResults(result, "WebApi should only depend on Module Entry projects, not directly on .Core or .Infrastructure layers.");
    }
}