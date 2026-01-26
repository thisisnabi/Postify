using NetArchTest.Rules;

namespace Postify.ArchitectureTests;

public class ModuleIsolationTests : BaseTest
{
    [Theory]
    [MemberData(nameof(GetModuleNames), MemberType = typeof(BaseTest))]
    public void Module_Should_Be_Isolated_From_Other_Modules(string moduleName)
    {
        // Arrange
        var currentModuleAssemblies = AllAssemblies
            .Where(a => a.GetName().Name!.Contains($".Modules.{moduleName}"))
            .ToArray();

        var otherModuleNames = ModuleNames
            .Where(m => m != moduleName)
            .Select(m => $"Postify.Modules.{m}")
            .ToArray();

        // Act
        var result = Types.InAssemblies(currentModuleAssemblies)
            .ShouldNot()
            .HaveDependencyOnAny(otherModuleNames)
            .GetResult();

        // Assert
        AssertArchResults(result, $"Module '{moduleName}' should not depend on other modules directly");
    }
}