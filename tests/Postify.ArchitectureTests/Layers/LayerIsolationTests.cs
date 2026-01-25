using NetArchTest.Rules;
using Postify.ArchitectureTests.Base;

namespace Postify.ArchitectureTests.Layers;

public class LayerIsolationTests : BaseTest
{
    [Fact]
    public void Core_Should_Not_Depend_On_Infrastructure_Or_EF()
    {
        // Arrange
        var infraNames = InfrastructureAssemblies.Select(a => a.GetName().Name!).ToArray();

        // Act
        var result = Types.InAssemblies(CoreAssemblies)
            .ShouldNot()
            .HaveDependencyOnAny(infraNames)
            .Or().HaveDependencyOn("Microsoft.EntityFrameworkCore")
            .GetResult();

        // Assert
        AssertArchResults(result, "Core layer must not depend on Infrastructure or EF Core");
    }

    [Fact]
    public void Controllers_Should_Not_Depend_On_Infrastructure()
    {
        // Arrange
        var infraNames = InfrastructureAssemblies.Select(a => a.GetName().Name!).ToArray();

        // Act
        var result = Types.InAssemblies(ModuleEntryAssemblies)
            .That().HaveNameEndingWith("Controller")
            .ShouldNot().HaveDependencyOnAny(infraNames)
            .GetResult();

        // Assert
        AssertArchResults(result, "API Controllers should not depend on Infrastructure directly");
    }

    [Fact]
    public void DbContexts_Should_Be_Internal()
    {
        // Act
        var result = Types.InAssemblies(InfrastructureAssemblies)
            .That().Inherit(typeof(Microsoft.EntityFrameworkCore.DbContext))
            .Should().NotBePublic()
            .GetResult();

        // Assert
        AssertArchResults(result, "DbContexts must be internal");
    }

    [Fact]
    public void WebApi_Should_Not_Touch_Infrastructure()
    {
        // Arrange
        Assert.NotNull(WebApiAssembly);
        var infraNames = InfrastructureAssemblies.Select(a => a.GetName().Name!).ToArray();

        // Act
        var result = Types.InAssembly(WebApiAssembly)
            .ShouldNot().HaveDependencyOnAny(infraNames)
            .GetResult();

        // Assert
        AssertArchResults(result, "WebApi should not have direct dependencies on Infrastructure");
    }
}