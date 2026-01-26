using System.Reflection;
using NetArchTest.Rules;

namespace Postify.ArchitectureTests.Base;

public abstract class BaseTest
{
    // Simple dynamic discovery of Postify assemblies
    protected static readonly Assembly[] AllAssemblies = LoadProjectAssemblies();

    private static Assembly[] LoadProjectAssemblies()
    {
        var path = AppDomain.CurrentDomain.BaseDirectory;
        
        return Directory.GetFiles(path, "Postify.*.dll")
            .Where(file => !Path.GetFileName(file).Contains(".ArchitectureTests")) // Exclude self
            .Select(file =>
            {
                try { return Assembly.LoadFrom(file); }
                catch { return null; }
            })
            .OfType<Assembly>()
            .Distinct()
            .ToArray();
    }

    protected static readonly Assembly[] CoreAssemblies = AllAssemblies
        .Where(a => a.GetName().Name?.EndsWith(".Core") == true)
        .ToArray();

    protected static readonly Assembly[] InfrastructureAssemblies = AllAssemblies
        .Where(a => a.GetName().Name?.EndsWith(".Infrastructure") == true)
        .ToArray();

    protected static readonly Assembly[] ModuleEntryAssemblies = AllAssemblies
        .Where(IsModuleEntryAssembly)
        .ToArray();

    protected static readonly string[] ModuleNames = AllAssemblies
        .Where(a => a.GetName().Name?.Contains(".Modules.") == true)
        .Select(a => a.GetName().Name!.Split('.')[2])
        .Distinct()
        .ToArray();

    protected static readonly Assembly? WebApiAssembly = AllAssemblies
        .FirstOrDefault(a => a.GetName().Name == "Postify.WebApi");

    private static bool IsModuleEntryAssembly(Assembly assembly)
    {
        var name = assembly.GetName().Name;
        return name != null && 
               name.Contains(".Modules.") && 
               !name.EndsWith(".Core") && 
               !name.EndsWith(".Infrastructure");
    }

    public static TheoryData<string> GetModuleNames()
    {
        var data = new TheoryData<string>();
        foreach (var name in ModuleNames) data.Add(name);
        return data;
    }

    // Simple, clean assertion with human-readable logging
    protected void AssertArchResults(TestResult result, string description)
    {
        if (result.IsSuccessful) return;

        var failingTypes = result.FailingTypeNames ?? Enumerable.Empty<string>();
        
        var message = $"""
            
            [ARCHITECTURAL VIOLATION]
            Issue: {description}
            
            Failing Classes:
            {string.Join(Environment.NewLine, failingTypes.Select(t => $" - {t}"))}
            ----------------------
            """;

        Assert.True(result.IsSuccessful, message);
    }
}