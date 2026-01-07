using System.Reflection;
using NetArchTest.Rules;
using Postify.ArchitectureTests.Base;

namespace Postify.ArchitectureTests.Layers;

public class LayerIsolationTests : BaseTest
{
    [Fact]
    public void Core_Should_Not_Have_Dependency_On_Infrastructure_And_EF()
    {
        var infraNames = InfrastructureAssemblies.Select(a => a.GetName().Name).ToArray();

        var result = Types.InAssemblies(CoreAssemblies)
            .ShouldNot()
            .HaveDependencyOnAny(infraNames) 
            .Or().HaveDependencyOn("Microsoft.EntityFrameworkCore") 
            .GetResult();

        var failingTypes = result.FailingTypeNames != null 
            ? string.Join("\n - ", result.FailingTypeNames) : "None";

        Assert.True(result.IsSuccessful, 
            $"Clean Architecture Violation: Core layer must not touch Infrastructure or EF Core.\nFailing Classes:\n - {failingTypes}");
    }

    [Fact]
    public void Controllers_Should_Not_Depend_On_Infrastructure()
    {
        var infraNames = InfrastructureAssemblies.Select(a => a.GetName().Name).ToArray();

        var result = Types.InAssemblies(ModuleEntryAssemblies)
            .That().HaveNameEndingWith("Controller")
            .ShouldNot().HaveDependencyOnAny(infraNames)
            .GetResult();

        Assert.True(result.IsSuccessful, $"Violation: Controllers touching Infrastructure directly: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void DbContexts_Should_Be_Internal()
    {
        var result = Types.InAssemblies(InfrastructureAssemblies)
            .That().Inherit(typeof(Microsoft.EntityFrameworkCore.DbContext))
            .Should().NotBePublic()
            .GetResult();

        Assert.True(result.IsSuccessful, $"Violation: These DbContexts must be internal: {string.Join(", ", result.FailingTypeNames ?? [])}");
    }

    [Fact]
    public void WebApi_Should_Not_Touch_Infrastructure_Persistence()
    {
        var infraNames = InfrastructureAssemblies.Select(a => a.GetName().Name).ToArray();

        var result = Types.InAssembly(WebApiAssembly) 
            .ShouldNot()
            .HaveDependencyOnAny(infraNames)
            .GetResult();

        var failingTypes = result.FailingTypeNames != null 
            ? string.Join("\n - ", result.FailingTypeNames) : "None";

        Assert.True(result.IsSuccessful, 
            $"Violation: WebApi should only talk to Entry Modules, never Infrastructure Persistence!\n" +
            $"Classes failing:\n - {failingTypes}");
    }
    
}