using System.Reflection;
using NetArchTest.Rules;
using Postify.ArchitectureTests.Base;

namespace Postify.ArchitectureTests.Modules;

public class ModuleDependencyTests : BaseTest
{
    [Theory]
    [MemberData(nameof(GetModuleNames), MemberType = typeof(BaseTest))]
    public void Module_ShouldBe_Isolated_From_Other_Modules(string moduleName)
    {
        var currentModuleAssemblies = AllAssemblies
            .Where(a => a.GetName().Name!.Contains($".Modules.{moduleName}"))
            .ToList();

        var otherModuleNames = ModuleNames.Where(m => m != moduleName).ToList();
        var violations = new List<string>();

        foreach (var otherModule in otherModuleNames)
        {
            var targetNamespace = $"Postify.Modules.{otherModule}";
            
            var result = Types.InAssemblies(currentModuleAssemblies)
                .ShouldNot()
                .HaveDependencyOn(targetNamespace)
                .GetResult();

            if (!result.IsSuccessful && result.FailingTypeNames != null)
            {
                foreach (var failingClass in result.FailingTypeNames)
                {
                    // گزارش: نشت بیزینس به همسایه
                    violations.Add($"   - Class [{failingClass}] illegally touches Module [{otherModule}]");
                }
            }
        }

        Assert.True(violations.Count == 0, 
            $"Modular Monolith Violation in [{moduleName}]:\n" + 
            "Modules must be isolated from each other. Use Pub/Sub or MediatR for communication.\n" +
            string.Join("\n", violations));
    }
}